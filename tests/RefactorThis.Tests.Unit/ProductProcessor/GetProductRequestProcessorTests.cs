using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.ProductProcessor;

namespace RefactorThis.Core.Unit.ProductProcessor
{
    [TestFixture]
    public class GetProductsRequestProcessorTests_ListProductsByName
    {
        private Mock<IProductRepository> _productRepo;
        [SetUp]
        public void Setup()
        {
            _productRepo = new Mock<IProductRepository>();
        }

        [Test]
        public async Task GivenProductNameExists_ShouldReturnExpectedProductsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var products = new List<Product> 
            { 
                new Product(id, "iPad", "Apple tablet", 1500, 10) 
            };
            var expectedProducts = new ProductsDto(products);

            var queryList = new List<Product>
            {
                new Product(id, "iPad", "Apple tablet", 1500, 10)
            };
            IList<Product> queryResult = queryList;

            _productRepo.Setup(x => x.ListAsync(It.IsAny<string>())).Returns(Task.FromResult(queryResult));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            ProductsDto result = await sut.ListProductsAsync("iPad");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProducts);
        }

        [Test]
        public async Task GivenProductListNull_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _productRepo.Setup(x => x.ListAsync(It.IsAny<string>())).Returns(Task.FromResult((IList<Product>)null));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act 
            Func<Task> act = async () => { await sut.ListProductsAsync("iPad"); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("There is no product");
        }

        [Test]
        public async Task GivenProductListEmpty_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var productList = new List<Product>();
            IList<Product> queryResult = productList;
            _productRepo.Setup(x => x.ListAsync(It.IsAny<string>())).Returns(Task.FromResult(queryResult));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act 
            Func<Task> act = async () => { await sut.ListProductsAsync("iPad"); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("There is no product");
        }

        [Test]
        public async Task GivenProductNameNullOrEmpty_ShouldListAllProducts()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };
            var expectedProductsDto = new ProductsDto(products);

            var queryList = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };
            IList<Product> queryResult = queryList;

            _productRepo = new Mock<IProductRepository>();
            _productRepo.Setup(x => x.ListAsync()).Returns(Task.FromResult(queryResult));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = await sut.ListProductsAsync(null);

            // Assert
            result.Should().NotBeNull();
            result.Items.Single(x => x.Id == id1).Should().BeEquivalentTo(expectedProductsDto.Items.Single(x => x.Id == id1));
            result.Items.Single(x => x.Id == id2).Should().BeEquivalentTo(expectedProductsDto.Items.Single(x => x.Id == id2));
        }
    }
    [TestFixture]
    public class GetProductRequestProcessorTests_GetProductById
    {
        private Mock<IProductRepository> _productRepo;

        [SetUp]
        public void Setup()
        {
            _productRepo = new Mock<IProductRepository>();
        }

        [Test]
        public async Task GivenValidId_ShouldReturnProduct()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedProduct = new Product(id, "iPad", "Apple tablet", 800, 15);
            var queryResult = new Product(id, "iPad", "Apple tablet", 800, 15);
            _productRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(queryResult));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = await sut.GetProductByIdAsync(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProduct);
        }

        [Test]
        public async Task GivenIdNonExists_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _productRepo.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Product)null));
            var sut = new GetProductRequestProcessor(_productRepo.Object);

            // Act 
            Func<Task> act = async () => { await sut.GetProductByIdAsync(It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Product not found");
        }
    }
}
