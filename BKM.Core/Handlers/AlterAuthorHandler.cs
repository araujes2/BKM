using AutoMapper;
using BKM.Core.Commands;
using BKM.Core.DTO;
using BKM.Core.Entities;
using BKM.Core.Generic;
using BKM.Core.Interfaces;
using BKM.Core.Responses;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.Core.Handlers
{
    public class AlterAuthorHandler : IAlterAuthorHandler
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        public AlterAuthorHandler(IRepositoryProvider repositoryProvider, IMapper mapper, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CreateOrAlterAuthorResponse> Handle(AlterAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateOrAlterAuthorResponse()
            {
                Status = 200,
                Date = DateTime.Now,
                Message = "Successful"
            };

            try
            {
                var validation = new AlterAuthorCommandValidation(_repositoryProvider, command);

                if (validation.IsValid())
                {

                    var author = _mapper.Map<Author>(command);
                    _repositoryProvider.Author.Edit(author);
                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnAuthorAltered(author);

                    response.Result = _mapper.Map<DtoAuthor>(author);
                }
                else
                {
                    response.Status = 400;
                    response.Message = string.Join("; ", validation.Errors);
                }

            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);

        }

        private void OnAuthorAltered(Author author)
        {
            _cache.Set(CacheKeys.Authors, _repositoryProvider.Author.Load().ToList());
        }
    }
}
