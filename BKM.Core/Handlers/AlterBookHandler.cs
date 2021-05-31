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
    public class AlterBookHandler : IAlterBookHandler
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        public AlterBookHandler(IRepositoryProvider repositoryProvider, IMapper mapper, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CreateOrAlterBookResponse> Handle(AlterBookCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateOrAlterBookResponse()
            {
                Status = 200,
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                var validation = new AlterBookCommandValidation(_repositoryProvider, command);

                if (validation.IsValid())
                {
                    var book = _mapper.Map<Book>(command);
                    _repositoryProvider.Book.Edit(book);

                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnBookAltered(book);

                    response.Result = _mapper.Map<DtoBook>(book);
                }
                else
                {
                    response.Status = 400;
                    response.Message = string.Join("; ", validation.Errors);
                }

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);
        }

        private void OnBookAltered(Book book)
        {
            _cache.Set(CacheKeys.Books, _repositoryProvider.Book.Load().ToList());
        }
    }
}
