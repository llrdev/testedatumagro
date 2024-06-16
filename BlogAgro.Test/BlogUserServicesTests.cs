using BlogAgro.Data.Repository.Interfaces;
using BlogAgro.Domain.Entity;
using BlogAgro.Services;
using Moq;
using System.Linq.Expressions;

namespace BlogAgro.Test
{
    public class BlogUserServicesTests
    {
        private readonly Mock<IBlogUserRepository> _mockRepository;
        private readonly BlogUserServices _service;

        public BlogUserServicesTests()
        {
            _mockRepository = new Mock<IBlogUserRepository>();
            _service = new BlogUserServices(_mockRepository.Object);
        }

        [Fact]
        public async Task Add_ShouldThrowException_WhenUserWithEmailExists()
        {
            // Arrange
            var entity = new BlogUserEntity { Email = "leandro.datum@agro.com.br" };
            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<BlogUserEntity, bool>>>()))
                .ReturnsAsync(new List<BlogUserEntity> { entity });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Add(entity));
            Assert.Equal($"Entity Found with email {entity.Email}", exception.Message);
        }

        [Fact]
        public async Task Add_ShouldAddUser_WhenUserWithEmailDoesNotExist()
        {
            // Arrange
            var entity = new BlogUserEntity { Email = "leandro.datum@agro.com.br" };
            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<BlogUserEntity, bool>>>()))
                .ReturnsAsync(new List<BlogUserEntity>());
            _mockRepository.Setup(repo => repo.AddAsync(entity)).ReturnsAsync(entity);

            // Act
            var result = await _service.Add(entity);

            // Assert
            Assert.Equal(entity, result);
            _mockRepository.Verify(repo => repo.AddAsync(entity), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var entity = new BlogUserEntity { Id = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id))
                .ReturnsAsync((BlogUserEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Update(entity));
            Assert.Equal("Entity Not Found", exception.Message);
        }

        [Fact]
        public async Task Update_ShouldUpdateUser_WhenUserExists()
        {
            // Arrange
            var existingEntity = new BlogUserEntity { Id = 1 };
            var updatedEntity = new BlogUserEntity { Id = 1, Email = "leandro.update.datum@agro.com.br" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(existingEntity.Id))
                .ReturnsAsync(existingEntity);
            _mockRepository.Setup(repo => repo.UpdateAsync(existingEntity))
                .ReturnsAsync(updatedEntity);

            // Act
            var result = await _service.Update(updatedEntity);

            // Assert
            Assert.Equal(updatedEntity, result);
            _mockRepository.Verify(repo => repo.UpdateAsync(existingEntity), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            int entityId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(entityId))
                .ReturnsAsync((BlogUserEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Delete(entityId));
            Assert.Equal("Entity Not Found", exception.Message);
        }

        [Fact]
        public async Task Delete_ShouldDeleteUser_WhenUserExists()
        {
            // Arrange
            var entity = new BlogUserEntity { Id = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id))
                .ReturnsAsync(entity);
            _mockRepository.Setup(repo => repo.RemoveAsync(entity.Id))
                .ReturnsAsync(1);

            // Act
            var result = await _service.Delete(entity.Id);

            // Assert
            Assert.Equal(1, result);
            _mockRepository.Verify(repo => repo.RemoveAsync(entity.Id), Times.Once);
        }

        [Fact]
        public async Task List_ShouldReturnAllUsers()
        {
            // Arrange
            var entities = new List<BlogUserEntity>
        {
            new BlogUserEntity { Id = 1, Email = "leandro.datum@agro.com.br" },
            new BlogUserEntity { Id = 2, Email = "leandroLopes.datum@agro.com.br" }
        };
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(entities);

            // Act
            var result = await _service.List();

            // Assert
            Assert.Equal(entities, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var entity = new BlogUserEntity { Id = 1 };
            _mockRepository.Setup(repo => repo.GetByIdAsync(entity.Id))
                .ReturnsAsync(entity);

            // Act
            var result = await _service.GetById(entity.Id);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetByEmail_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            string email = "leandro.datum@agro.com.br";
            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<BlogUserEntity, bool>>>()))
                .ReturnsAsync((List<BlogUserEntity>)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetByEmail(email));
            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task GetByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var entity = new BlogUserEntity { Email = "leandro.datum@agro.com.br" };
            _mockRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<BlogUserEntity, bool>>>()))
                .ReturnsAsync(new List<BlogUserEntity> { entity });

            // Act
            var result = await _service.GetByEmail(entity.Email);

            // Assert
            Assert.Equal(entity, result);
        }
    }
}
