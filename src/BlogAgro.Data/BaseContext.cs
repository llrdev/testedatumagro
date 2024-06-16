﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BlogAgro.Data.Configuration;
using BlogAgro.Domain.Entity;

namespace BlogAgro.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BlogEntityConfiguration());
            builder.ApplyConfiguration(new BlogUserEntityConfiguration());

            base.OnModelCreating(builder);
            
        }

        public DbSet<BlogEntity> Blog { get; set; }
        public DbSet<BlogUserEntity> BlogUser { get; set; }
    }
}
