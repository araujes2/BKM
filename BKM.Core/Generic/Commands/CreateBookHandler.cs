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

namespace BKM.Core.Generic
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommandRequest, CreateBookResponse>
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

        public async Task<CreateBookResponse> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateBookResponse()
            {
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                var book = _repositoryProvider.Book.Add(new Book()
                {
                    AuthorID = request.AuthorID,
                    Category = request.Category,
                    ISBM = request.ISBM,
                    LaunchDate = request.LaunchDate,
                    Title = request.Title                    
                });

                await _repositoryProvider.UoW.SaveChangesAsync();

                OnBookAdded(book);

                response.Result = _mapper.Map<DtoBook>(book);

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
