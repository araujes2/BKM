using AutoMapper;
using BKM.Core.DTO;
using BKM.Core.Entities;
using BKM.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.API
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;
        public CreateAuthorHandler(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }
        public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateAuthorResponse()
            {
                Date = DateTime.Now,
                Requester = request.Requester,
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

                await _repositoryProvider.UoW.SaveChangesAsync();

                await OnAuthorAdded(author);

                response.Result = _mapper.Map<DtoAuthor>(author);

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);

        }

        private Task OnAuthorAdded(Author author)
        {
            return Task.CompletedTask;
        }

    }
}
