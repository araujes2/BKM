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

        [HttpGet("{ISBM}")]
        public async Task<Book> Get([FromQuery] string ISBM)
        {
            _logger.LogInformation($"Executing [GET] api/Books/{ISBM} at { DateTime.Now  }");
            return await _repositoryProvider.Book.Load().FirstOrDefaultAsync(m => m.ISBM == ISBM);
        }

        [HttpPost]
        public async Task<CreateBookResponse> Post([FromServices] IMediator mediator, [FromBody] CreateBookCommandRequest request)
        {
            _logger.LogInformation($"Executing [POST] api/Books at { DateTime.Now  }");
            return await mediator.Send(request);
        }

    }
}
