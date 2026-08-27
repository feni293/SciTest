using Moq;
using SCITest.Application.Interfaces.Repositories;
using SCITest.Domain.Entities;
using SCITest.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCITest.Tests.Services
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateProduct_WhenProductIsValid()
        {
            // Arrange
            var product = new Product
            {
                Name = "Laptop",
                Description = "Gaming laptop",
                Price = 4500000
            };

            _repositoryMock
                .Setup(x => x.CreateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product p, CancellationToken _) =>
                {
                    p.Id = 1;
                    p.CreatedDate = DateTime.UtcNow;
                    return p;
                });

            // Act
            var result = await _service.CreateAsync(
                product,
                CancellationToken.None);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(4500000, result.Price);

            _repositoryMock.Verify(
                x => x.CreateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenNameIsEmpty()
        {
            // Arrange
            var product = new Product
            {
                Name = "",
                Price = 100
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(
                () => _service.CreateAsync(
                    product,
                    CancellationToken.None));

            Assert.Equal(
                "Product name is required.",
                exception.Message);

            _repositoryMock.Verify(
                x => x.CreateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenPriceIsInvalid()
        {
            // Arrange
            var product = new Product
            {
                Name = "Laptop",
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => _service.CreateAsync(
                    product,
                    CancellationToken.None));

            _repositoryMock.Verify(
                x => x.CreateAsync(
                    It.IsAny<Product>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 4500000
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(
                    1,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(
                1,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetByIdAsync(
                    999,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(
                999,
                CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenProductIsDeleted()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.DeleteAsync(
                    1,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(
                1,
                CancellationToken.None);

            // Assert
            Assert.True(result);
        }
    }
}
