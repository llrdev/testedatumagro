using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.DTO;
using BlogAgro.Domain.Entity;
using BlogAgro.Services;
using Moq;

namespace BlogAgro.Test
{
    public class BlogServicesTests
    {
        private readonly Mock<IBlogRepository> _mockRepository;
        private readonly BlogServices _service;

        public BlogServicesTests()
        {
            _mockRepository = new Mock<IBlogRepository>();
            _service = new BlogServices(_mockRepository.Object);
        }

        [Fact]
        public async Task Add_ShouldAddBlog()
        {
            // Arrange
            var entity = new BlogEntity { Id = 1, Title = "Test Blog" };
            _mockRepository.Setup(repo => repo.AddAsync(entity)).ReturnsAsync(entity);

            // Act
            var result = await _service.Add(entity);

            // Assert
            Assert.Equal(entity, result);
            _mockRepository.Verify(repo => repo.AddAsync(entity), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenBlogNotFound()
        {
            // Arrange
            var entity = new BlogUpdateDTO { Id = 1, Title = "Updated Blog" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id)).ReturnsAsync((BlogEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Update(entity));
            Assert.Equal("Entity Not Found", exception.Message);
        }

        [Fact]
        public async Task Update_ShouldUpdateBlog_WhenBlogExists()
        {
            // Arrange
            var existingEntity = new BlogEntity { Id = 1, Title = "Old Blog" };
            var updateDTO = new BlogUpdateDTO { Id = 1, Title = "Updated Blog" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(existingEntity.Id)).ReturnsAsync(existingEntity);
            _mockRepository.Setup(repo => repo.UpdateAsync(existingEntity)).ReturnsAsync(existingEntity);

            // Act
            var result = await _service.Update(updateDTO);

            // Assert
            Assert.Equal(existingEntity, result);
            _mockRepository.Verify(repo => repo.UpdateAsync(existingEntity), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldThrowException_WhenBlogNotFound()
        {
            // Arrange
            int entityId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(entityId)).ReturnsAsync((BlogEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Delete(entityId));
            Assert.Equal("Entity Not Found", exception.Message);
        }

        [Fact]
        public async Task Delete_ShouldRemoveBlog_WhenBlogExists()
        {
            // Arrange
            var entity = new BlogEntity { Id = 1, Title = "Test Blog" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id)).ReturnsAsync(entity);
            _mockRepository.Setup(repo => repo.RemoveAsync(entity.Id)).ReturnsAsync(1);

            // Act
            var result = await _service.Delete(entity.Id);

            // Assert
            Assert.Equal(1, result);
            _mockRepository.Verify(repo => repo.RemoveAsync(entity.Id), Times.Once);
        }

        [Fact]
        public async Task List_ShouldReturnAllBlogs()
        {
            // Arrange
            var entities = new List<BlogEntity>
        {
            new BlogEntity { Id = 1, Title = "Blog 1" },
            new BlogEntity { Id = 2, Title = "Blog 2" }
        };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entities);

            // Act
            var result = await _service.List();

            // Assert
            Assert.Equal(entities, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnBlog_WhenBlogExists()
        {
            // Arrange
            var entity = new BlogEntity { Id = 1, Title = "Test Blog" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            // Act
            var result = await _service.GetById(entity.Id);

            // Assert
            Assert.Equal(entity, result);
        }
    }
}
