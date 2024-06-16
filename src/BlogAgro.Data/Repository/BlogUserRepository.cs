using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.Entity;

namespace BlogAgro.Data.Repository
{
    public class BlogUserRepository : EntityBaseRepository<BlogUserEntity>, IBlogUserRepository
    {
        private readonly ApplicationQuerieContext _querieContext;
        private readonly ApplicationCommandContext _commandContext;

        public BlogUserRepository(ApplicationQuerieContext querieContext, ApplicationCommandContext commandContext) : base(querieContext, commandContext)
        {
            _querieContext = querieContext;
            _commandContext = commandContext;
        }
    }
}
