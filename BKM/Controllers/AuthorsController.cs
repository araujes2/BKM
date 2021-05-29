using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using BKM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using BKM.Core.Entities;
using BKM.Core.Generic;
using Microsoft.Extensions.Caching.Memory;
using BKM.Core.Commands;
using BKM.API.Utilities;

namespace BKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryProvider _repositoryProvider;
        public AuthorsController([FromServices] IRepositoryProvider repositoryProvider, ILogger<AuthorsController> logger)
        {
            _logger = logger;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> Get([FromServices] IMemoryCache cache)
        {
            _logger.LogInformation($"Executing [GET] api/authors at { DateTime.Now  }");
            return await cache.GetOrCreateAsync(CacheKeys.Authors, entry =>
            {
                return _repositoryProvider.Author.Load().ToListAsync();
            });
        }

        [HttpGet("{ID}", Name = "GetAuthor")]
        public async Task<IActionResult> Get([FromQuery] string ID)
        {
            _logger.LogInformation($"Executing [GET] api/authors/{ID} at { DateTime.Now  }");
            return Ok(await _repositoryProvider.Author.Load().FirstOrDefaultAsync(m => m.ID == ID));
        }

        [HttpPost]
        [ServiceFilter(typeof(QueueMessageActionFilter))]
        public async Task<IActionResult> Post([FromServices] IMediator mediator, [FromBody] CreateAuthorCommand command)
        {
            _logger.LogInformation($"Executing [POST] api/authors at { DateTime.Now  }");
            var output = await mediator.Send(command);
            return StatusCode(output.Status, output);
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete([FromServices] IMediator mediator, [FromQuery] DeleteAuthorCommand command)
        {
            _logger.LogInformation($"Executing [DELETE] api/authors/{command.ID} at { DateTime.Now  }");
            var output = await mediator.Send(command);
            return StatusCode(output.Status, output);
        }

    }
}
