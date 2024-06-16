using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;
using BlogAgro.Services.Services.Interfaces;

namespace BlogAgro.Services
{

    public class BlogServices : IBlogServices
    {
        private readonly IBlogRepository _blogRepository;

        public BlogServices(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }
        public async Task<BlogEntity> Add(BlogEntity entity)
        {
            return await _blogRepository.AddAsync(entity);
        }
        public async Task<BlogEntity> Update(BlogUpdateDTO entity)
        {
            var _entity = await _blogRepository.GetByIdAsync(entity.Id) ?? throw new Exception("Entity Not Found");
            _entity.Update(entity);

            return await _blogRepository.UpdateAsync(_entity);
        }
        public async Task<int> Delete(int entityId)
        {
            var _entity = await _blogRepository.GetByIdAsync(entityId) ?? throw new Exception("Entity Not Found");
            return await _blogRepository.RemoveAsync(entityId);
        }
        public async Task<IEnumerable<BlogEntity>> List()
        {
            return await _blogRepository.GetAllAsync();
        }
        public async Task<BlogEntity> GetById(int entityId)
        {
            return await _blogRepository.GetByIdAsync(entityId);
        }
    }
}
