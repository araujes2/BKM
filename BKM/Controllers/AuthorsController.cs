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
using Microsoft.AspNetCore.Authorization;
using BKM.Core.Notifications;
using Microsoft.Extensions.Options;

namespace BKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryProvider _repositoryProvider;
        public AuthorsController(IRepositoryProvider repositoryProvider, ILogger<AuthorsController> logger)
        {
            _logger = logger;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Author>> GetAsync([FromServices] IMemoryCache cache)
        {
            _logger.LogInformation($"Executing [GET] api/authors at { DateTime.Now  }");

            return await cache.GetOrCreateAsync(CacheKeys.Authors, entry =>
            {
                return _repositoryProvider.Author.Load().ToListAsync();
            });
        }

        [HttpGet("{ID}", Name = "GetAuthor")]
        public async Task<IActionResult> GetAsync([FromQuery] string ID)
        {
            _logger.LogInformation($"Executing [GET] api/authors/{ID} at { DateTime.Now  }");

            var output = await _repositoryProvider.Author.Load().FirstOrDefaultAsync(m => m.ID == ID);

            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] IMediator mediator, [FromServices] IOptions<ServiceOptions> options, [FromBody] CreateAuthorCommand command)
        {
            _logger.LogInformation($"Executing [POST] api/authors at { DateTime.Now  }");

            command.Today = DateTime.Today;

            var output = await mediator.Send(command);

            await mediator.Publish(new CreateEntityNotification
            {
                Entity = "Author",
                Object = output.Result,
                QueueName = options.Value.QueueName,
                StorageConnectionString = options.Value.StorageConnectionString
            });

            return StatusCode(output.Status, output);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromServices] IMediator mediator, [FromBody] AlterAuthorCommand command)
        {
            _logger.LogInformation($"Executing [PUT] api/authors at { DateTime.Now  }");

            command.Today = DateTime.Today;

            var output = await mediator.Send(command);

            return StatusCode(output.Status, output);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync([FromServices] IMediator mediator, [FromBody] DeleteAuthorCommand command)
        {
            _logger.LogInformation($"Executing [DELETE] api/authors/{command.ID} at { DateTime.Now  }");
            var output = await mediator.Send(command);
            return StatusCode(output.Status, output);
        }

    }
}
