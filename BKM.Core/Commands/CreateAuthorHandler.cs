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
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorResponse>
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
        public async Task<CreateAuthorResponse> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = new CreateAuthorResponse()
            {
                Status = 200,
                Date = DateTime.Now,
                Message = "Successful"
            };

            try
            {
                if(command.IsValid())
                {
                    if (_repositoryProvider.Author.Load().Any(m => m.ID == command.ID))
                    {
                        response.Status = 400;
                        throw new Exception("Althor already exists");
                    }

                    var author = _repositoryProvider.Author.Add(new Author()
                    {
                        ID = command.ID,
                        Name = command.Name,
                        DateOfBirth = command.DateOfBirth
                    });

                    _repositoryProvider.UoW.SaveChanges();

                    OnAuthorAdded(author);

                    response.Result = _mapper.Map<DtoAuthor>(author);
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Invalid Command";
                }

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
