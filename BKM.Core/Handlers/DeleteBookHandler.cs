using AutoMapper;
using BKM.Core.Commands;
using BKM.Core.Generic;
using BKM.Core.Interfaces;
using BKM.Core.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.Core.Handlers
{
    public class DeleteBookHandler : IDeleteBookHandler
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        public DeleteBookHandler(IRepositoryProvider repositoryProvider, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _cache = cache;
        }

        public async Task<DeleteBookResponse> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteBookResponse()
            {
                Status = 200,
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                if (_repositoryProvider.Book.Load().FirstOrDefault(m => m.ISBM == command.ISBM) != null)
                {
                    _repositoryProvider.Book.Remove(command.ISBM);

                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnBookRemoved();

                    response.ISBM = command.ISBM;
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Book not found";
                }

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);
        }

        private void OnBookRemoved()
        {
            _cache.Set(CacheKeys.Books, _repositoryProvider.Book.Load().ToList());
        }
    }
}
