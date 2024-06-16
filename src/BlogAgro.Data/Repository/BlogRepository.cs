using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.Entity;

namespace BlogAgro.Data.Repository
{
    public class BlogRepository : EntityBaseRepository<BlogEntity>, IBlogRepository
    {
        private readonly ApplicationQuerieContext _querieContext;
        private readonly ApplicationCommandContext _commandContext;

        public BlogRepository(ApplicationQuerieContext querieContext, ApplicationCommandContext commandContext) : base(querieContext, commandContext)
        {
            _querieContext = querieContext;
            _commandContext = commandContext;
        }
    }
}
