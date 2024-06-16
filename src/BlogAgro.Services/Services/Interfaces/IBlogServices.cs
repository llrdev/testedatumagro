﻿using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;

namespace BlogAgro.Services.Services.Interfaces
{
    public interface IBlogServices
    {
        public Task<BlogEntity> Add(BlogEntity entity);

        public Task<BlogEntity> Update(BlogUpdateDTO entity);

        public Task<int> Delete(int entityId);

        public Task<IEnumerable<BlogEntity>> List();

        public Task<BlogEntity> GetById(int entityId);

    }
}
