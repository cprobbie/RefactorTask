using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

using Moq;

using NUnit.Framework;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Processor;

namespace RefactorThis.Core.Unit.Processor
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
        public void GivenProductNameExists_ShouldReturnExpectedProductsResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var products = new List<Product> 
            { 
                new Product(id, "iPad", "Apple tablet", 1500, 10) 
            };
            var expectedProducts = new ProductsDTO(products);

            var queryResult = new List<Product>
            {
                new Product(id, "iPad", "Apple tablet", 1500, 10)
            };

            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns(queryResult);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            ProductsDTO result = SUT.ListProducts("iPad");

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProducts);
        }

        [Test]
        public void GivenProductListNull_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns((IList<Product>)null);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts("iPad");
            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void GivenProductListEmpty_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns(new List<Product>());
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts("iPad");
            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void GivenProductNameNullOrEmpty_ShouldListAllProducts()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };
            var expectedProductsDTO = new ProductsDTO(products);

            var queryResult = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };

            _productRepo = new Mock<IProductRepository>();
            _productRepo.Setup(x => x.List()).Returns(queryResult);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts(null);

            // Assert
            result.Should().NotBeNull();
            result.Items.Single(x => x.Id == id1).Should().BeEquivalentTo(expectedProductsDTO.Items.Single(x => x.Id == id1));
            result.Items.Single(x => x.Id == id2).Should().BeEquivalentTo(expectedProductsDTO.Items.Single(x => x.Id == id2));
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
        public void GivenValidId_ShouldReturnProduct()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedProduct = new Product(id, "iPad", "Apple tablet", 800, 15);
            var queryResult = new Product(id, "iPad", "Apple tablet", 800, 15);
            _productRepo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(queryResult);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.GetProductById(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProduct);
        }

        [Test]
        public void GivenIdNonExists_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.GetProductById(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
        }
    }
}
