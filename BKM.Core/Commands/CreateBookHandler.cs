using AutoMapper;
using BKM.Core.DTO;
using BKM.Core.Entities;
using BKM.Core.Generic;
using BKM.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.Core.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, CreateBookResponse>
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        public CreateBookHandler(IRepositoryProvider repositoryProvider, IMapper mapper, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<CreateBookResponse> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateBookResponse()
            {
                Status = 201,
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                var validation = new CreateBookCommandValidation(_repositoryProvider, command);

                if (validation.IsValid())
                {
                    var book = _repositoryProvider.Book.Add(new Book()
                    {
                        AuthorID = command.AuthorID,
                        Category = command.Category,
                        ISBM = command.ISBM,
                        LaunchDate = command.LaunchDate,
                        Title = command.Title
                    });

                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnBookAdded(book);

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

        private void OnBookAdded(Book book)
        {
            UpdateCache(book);
            SendToServiceBus(book);
        }

        private void UpdateCache(Book book)
        {
            _cache.Set(CacheKeys.Books, _repositoryProvider.Book.Load().ToList());
        }
        private void SendToServiceBus(Book book)
        {
         
        }
    }
}
