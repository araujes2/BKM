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
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommandRequest, CreateAuthorResponse>
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        public CreateAuthorHandler(IRepositoryProvider repositoryProvider, IMapper mapper, IMemoryCache cache)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<CreateAuthorResponse> Handle(CreateAuthorCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAuthorResponse()
            {
                Date = DateTime.Now,
                Requester = "",
                Message = "Successful"
            };

            try
            {
                var author = _repositoryProvider.Author.Add(new Author()
                {
                    ID = request.ID,
                    Name = request.Name,
                    DateOfBirth = request.DateOfBirth
                });

                _repositoryProvider.UoW.SaveChanges();

                OnAuthorAdded(author);

                response.Result = _mapper.Map<DtoAuthor>(author);

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);

        }

        private void OnAuthorAdded(Author author)
        {
            UpdateCache(author);
            SendToServiceBus(author);
        }

        private void UpdateCache(Author author)
        {
            _cache.Set(CacheKeys.Authors, _repositoryProvider.Author.Load().ToList());
        }
        private void SendToServiceBus(Author author)
        {

        }

    }
}
