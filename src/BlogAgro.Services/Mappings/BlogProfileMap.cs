using AutoMapper;
using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;


namespace BlogAgro.Services.Mappings
{
    public class BlogProfileMap : Profile
    {
        public BlogProfileMap()
        {
            CreateMap<BlogDTO, BlogEntity>().ReverseMap();
        }
    }
}
