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
using BKM.Core.Notifications;
using Microsoft.Extensions.Options;

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
        public async Task<IEnumerable<Book>> GetAsync([FromServices] IMemoryCache cache)
        {
            _logger.LogInformation($"Executing [GET] api/Books at { DateTime.Now  }");

            return await cache.GetOrCreateAsync(CacheKeys.Books, entry =>
            {
                return _repositoryProvider.Book.Load().ToListAsync();
            });
        }

        [HttpGet("{ISBM}", Name = "GetBook")]
        public async Task<IActionResult> GetAsync([FromQuery] string ISBM)
        {
            _logger.LogInformation($"Executing [GET] api/Books/{ISBM} at { DateTime.Now  }");

            var output = await _repositoryProvider.Book
                .Load()
                .FirstOrDefaultAsync(m => m.ISBM == ISBM);

            return Ok(output);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] IMediator mediator, [FromServices] IOptions<ServiceOptions> options, [FromBody] CreateBookCommand command)
        {
            _logger.LogInformation($"Executing [POST] api/books at { DateTime.Now  }");

            var output = await mediator.Send(command);

            await mediator.Publish(new CreateEntityNotification 
            { 
                Entity = "Book",
                Object = output.Result,
                QueueName = options.Value.QueueName,
                StorageConnectionString = options.Value.StorageConnectionString
            });

            return StatusCode(output.Status, output);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromServices] IMediator mediator, [FromBody] AlterBookCommand command)
        {
            _logger.LogInformation($"Executing [PUT] api/books at { DateTime.Now  }");

            var output = await mediator.Send(command);

            return StatusCode(output.Status, output);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync([FromServices] IMediator mediator, [FromBody] DeleteBookCommand command)
        {
            _logger.LogInformation($"Executing [DELETE] api/books/{command.ISBM} at { DateTime.Now  }");

            var output = await mediator.Send(command);

            return StatusCode(output.Status, output);
        }

    }
}
