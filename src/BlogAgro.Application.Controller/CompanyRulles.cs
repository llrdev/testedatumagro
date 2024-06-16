using BlogAgro.Domain.Entity;
using BlogAgro.Services.Services.Interfaces;

namespace BlogAgro.Application.Controller
{
    public class BlogRulles
    {
        private readonly IBlogServices _blogServices;

        public BlogRulles(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        private void ValidaCompania(BlogEntity  blogEntity) {

          var result = blogEntity.Text ?? throw new Exception("vazio ");

            if (result == null)
            {
                throw new Exception();
            }
        }

    }
}
