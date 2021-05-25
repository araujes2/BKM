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

        [HttpGet("{ID}")]
        public async Task<Author> Get([FromQuery] string ID)
        {
            _logger.LogInformation($"Executing [GET] api/authors/{ID} at { DateTime.Now  }");
            return await _repositoryProvider.Author.Load().FirstOrDefaultAsync(m => m.ID == ID);
        }

        [HttpPost]
        public async Task<CreateAuthorResponse> Post([FromServices] IMediator mediator, [FromBody] CreateAuthorCommandRequest request)
        {
            _logger.LogInformation($"Executing [POST] api/authors at { DateTime.Now  }");
            return await mediator.Send(request);
        }

    }
}
