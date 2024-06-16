using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAgro.Domain.Entity;

namespace BlogAgro.Data.Repository.Interfaces
{
    public interface IBlogUserRepository : IEntityRepository<BlogUserEntity>
    {
    }
}
