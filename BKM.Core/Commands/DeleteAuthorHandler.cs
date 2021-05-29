using AutoMapper;
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
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, DeleteAuthorResponse>
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        public DeleteAuthorHandler(IRepositoryProvider repositoryProvider, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _cache = cache;
        }

        public async Task<DeleteAuthorResponse> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteAuthorResponse()
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
                    var author = _repositoryProvider.Author.Load().FirstOrDefault(m => m.ID == command.ID);

                    if (author == null)
                    {
                        response.Status = 404;
                        throw new Exception("Author Not Found");
                    }

                    if (author.Books.Any())
                    {
                        response.Status = 400;
                        throw new Exception("Cannot remove author with books");
                    }

                    _repositoryProvider.Author.Remove(command.ID);

                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnAuthorRemoved();

                    response.ID = command.ID;
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

        private void OnAuthorRemoved()
        {
            _cache.Set(CacheKeys.Authors, _repositoryProvider.Author.Load().ToList());
        }
    }
}
