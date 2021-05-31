
using AutoMapper;
using BKM.Core.Commands;
using BKM.Core.DTO;
using BKM.Core.Entities;

namespace BKM.API.Utilities
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<Author, DtoAuthor>();
            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<AlterAuthorCommand, Author>();

            CreateMap<CreateBookCommand, Book>();
            CreateMap<AlterBookCommand, Book>();

            CreateMap<Book, DtoBook>()
                .ForMember(dest => dest.AuthorName, o => o.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.AuthorDateOfBirth, o => o.MapFrom(src => src.Author.DateOfBirth));
        }
    }
}
