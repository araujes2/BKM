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
    public class CreateBookHandler : IRequestHandler<CreateBookRequest, CreateBookResponse>
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;
        public CreateBookHandler(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }

        public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
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

                await OnBookAdded(book);

                response.Result = _mapper.Map<DtoBook>(book);

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return await Task.FromResult(response);
        }

        private async Task OnBookAdded(Book book)
        {
            await Task.CompletedTask;
        }
    }
}
