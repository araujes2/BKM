using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using BKM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using BKM.Core.Entities;
using Microsoft.Extensions.Caching.Memory;
using BKM.Core.Generic;
using BKM.Core.Commands;
using BKM.API.Utilities;

namespace BKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryProvider _repositoryProvider;
        public BooksController([FromServices] IRepositoryProvider repositoryProvider, ILogger<BooksController> logger)
        {
            _logger = logger;
            _repositoryProvider = repositoryProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get([FromServices] IMemoryCache cache)
        {
            _logger.LogInformation($"Executing [GET] api/Books at { DateTime.Now  }");
            return await cache.GetOrCreateAsync(CacheKeys.Books, entry =>
            {
                return _repositoryProvider.Book.Load().ToListAsync();
            });
        }

        [HttpGet("{ISBM}", Name = "GetBook")]
        public async Task<IActionResult> Get([FromQuery] string ISBM)
        {
            _logger.LogInformation($"Executing [GET] api/Books/{ISBM} at { DateTime.Now  }");
            return Ok(await _repositoryProvider.Book.Load().FirstOrDefaultAsync(m => m.ISBM == ISBM));
        }

        [HttpPost]
        [ServiceFilter(typeof(QueueMessageActionFilter))]
        public async Task<IActionResult> Post([FromServices] IMediator mediator, [FromBody] CreateBookCommand command)
        {
            _logger.LogInformation($"Executing [POST] api/books at { DateTime.Now  }");
            var output = await mediator.Send(command);
            return StatusCode(output.Status, output);
        }

        [HttpDelete("{ISBM}")]
        public async Task<IActionResult> Delete([FromServices] IMediator mediator, [FromQuery] DeleteBookCommand command)
        {
            _logger.LogInformation($"Executing [DELETE] api/books/{command.ISBM} at { DateTime.Now  }");
            var output = await mediator.Send(command);
            return StatusCode(output.Status, output);
        }

    }
}
