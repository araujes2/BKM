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
    public class DeleteAuthorHandler : IDeleteAuthorHandler
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
                var validation = new DeleteAuthorCommandValidation(_repositoryProvider, command);

                if (validation.IsValid())
                {
                    _repositoryProvider.Author.Remove(command.ID);

                    await _repositoryProvider.UoW.SaveChangesAsync();

                    OnAuthorRemoved();

                    response.ID = command.ID;
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

        private void OnAuthorRemoved()
        {
            _cache.Set(CacheKeys.Authors, _repositoryProvider.Author.Load().ToList());
        }
    }
}
