using AutoMapper;
using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;


namespace BlogAgro.Services.Mappings
{
    public class BlogUserProfileMap : Profile
    {
        public BlogUserProfileMap()
        {
            CreateMap<BlogUserDTO, BlogUserEntity>().ReverseMap();
            CreateMap<BlogUserRequestDTO, BlogUserEntity>().ReverseMap();
            
        }
    }
}
