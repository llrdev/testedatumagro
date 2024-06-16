using Asp.Versioning;
using AutoMapper;
using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;
using BlogAgro.Domain.Util;
using BlogAgro.Services;
using BlogAgro.Services.Mappings;
using BlogAgro.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BlogAgro.API.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserBlogController : ControllerBase
    {
        private readonly IBlogUserServices _blogUserServices;
        private IMapper mapper;
        public UserBlogController(IBlogUserServices blogUserServices)
        {
            _blogUserServices = blogUserServices;
            mapper = new MapperConfiguration(c => { c.AddProfile<BlogUserProfileMap>(); }).CreateMapper();
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BlogUserRequestDTO request)
        {

            try
            {

                var entity = mapper.Map<BlogUserEntity>(request);
                entity.Password = Security.Encrypt(request.Password, true);

                await _blogUserServices.Add(entity);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
