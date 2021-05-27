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
                Status = 200,
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                if(command.IsValid())
                {
                    if (_repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == command.AuthorID) == null)
                    {
                        response.Status = 404;
                        throw new Exception("Author Not Found");
                    }

                    if (_repositoryProvider.Book.Load().Any(m => m.ISBM == command.ISBM))
                    {
                        response.Status = 400;
                        throw new Exception("ISBM already exists");
                    }

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
                    response.Message = "Invalid Command";
                }

            }
            catch (Exception ex)
            {
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
            _cache.Set(CacheKeys.Books, _repositoryProvider.Author.Load().ToList());
        }
        private void SendToServiceBus(Book book)
        {
         
        }
    }
}
