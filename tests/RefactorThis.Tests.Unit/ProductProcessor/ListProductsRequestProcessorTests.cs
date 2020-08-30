using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Processor;

namespace RefactorThis.Core.Unit.Processor
{
    [TestFixture]
    public class ListProductsRequestProcessorTests_ListProducts
    {
        private Mock<IProductRepository> _productRepo;

        [Test]
        public void ShouldReturnExpectedProductsResult()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var productItems = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };
            var expectedProducts = new Products(productItems);

            var queryItems = new List<Product>
            {
                new Product(id1, "iPad", "Apple tablet", 1500, 10),
                new Product(id2, "MacBook", "Apple laptop", 2000, 15)
            };
            var queryResult = new Products(productItems);

            _productRepo = new Mock<IProductRepository>();
            _productRepo.Setup(x => x.List()).Returns(queryResult);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts();

            // Assert
            result.Should().NotBeNull();
            result.Items.Single(x => x.Id == id1).Should().BeEquivalentTo(expectedProducts.Items.Single(x => x.Id == id1));
            result.Items.Single(x => x.Id == id2).Should().BeEquivalentTo(expectedProducts.Items.Single(x => x.Id == id2));
        }
    }

    [TestFixture]
    public class ListProductsRequestProcessorTests_ListProductsByName
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
            var productItems = new List<Product>
            {
                new Product(id, "iPad", "Apple tablet", 1500, 10)
            };
            var expectedProducts = new Products(productItems);

            var queryItems = new List<Product>
            {
                new Product(id, "iPad", "Apple tablet", 1500, 10)
            };
            var queryProducts = new Products(productItems);

            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns(queryProducts);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts("iPad");

            // Assert
            result.Should().NotBeNull();
            result.Items.Single().Should().BeEquivalentTo(expectedProducts.Items.Single());
        }

        [Test]
        public void GivenProductNameNonExists_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns((Products)null);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts("iPad");
            // Assert
            result.Should().BeNull();
        }

        [Test]
        public void GivenProductNameNullOrEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            _productRepo.Setup(x => x.List(It.IsAny<string>()));
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            // Assert
            SUT.Invoking(x => x.ListProducts(null)).Should().Throw<ArgumentException>().WithMessage("Invalid Product name");
        }
    }
}
