using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.Entity;
using BlogAgro.Services.Services.Interfaces;

namespace BlogAgro.Services
{

    public class BlogUserServices : IBlogUserServices
    {
        private readonly IBlogUserRepository _blogUserRepository;

        public BlogUserServices(IBlogUserRepository blogUserRepository)
        {
            _blogUserRepository = blogUserRepository;
        }
        public async Task<BlogUserEntity> Add(BlogUserEntity entity)
        {
            var blogUser = await _blogUserRepository.GetAsync(predicate: f => f.Email == entity.Email);

            if (blogUser.Any())
            {
                throw new Exception($"Entity Found with email {entity.Email}");
            }

            return await _blogUserRepository.AddAsync(entity);
        }
        public async Task<BlogUserEntity> Update(BlogUserEntity entity)
        {
            var _entity = await _blogUserRepository.GetByIdAsync(entity.Id) ?? throw new Exception("Entity Not Found");

            _entity.Update(entity);

            return await _blogUserRepository.UpdateAsync(_entity);
        }
        public async Task<int> Delete(int entityId)
        {
            var _entity = await _blogUserRepository.GetByIdAsync(entityId) ?? throw new Exception("Entity Not Found");
            return await _blogUserRepository.RemoveAsync(entityId);
        }
        public async Task<IEnumerable<BlogUserEntity>> List()
        {
            return await _blogUserRepository.GetAllAsync();
        }
        public async Task<BlogUserEntity> GetById(int entityId)
        {
            return await _blogUserRepository.GetByIdAsync(entityId);
        }
        public async Task<BlogUserEntity> GetByEmail(string email)
        {
            var blogUser = await _blogUserRepository.GetAsync(predicate: f => f.Email == email);

            return blogUser == null ? throw new Exception("User not found") : blogUser?.FirstOrDefault();
        }
    }
}
