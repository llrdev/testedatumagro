using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAgro.Data
{
    public class ApplicationQuerieContext : BaseContext
    {
        public ApplicationQuerieContext(DbContextOptions<ApplicationQuerieContext> options) : base(options)
        {

        }
    }
}
