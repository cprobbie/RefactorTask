using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            var productItems = new List<Product>
            {
                new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1500, 10),
                new Product(Guid.NewGuid(), "MacBook", "Apple laptop", 2000, 15)
            };
            var expectedProducts = new Products(productItems);

            _productRepo = new Mock<IProductRepository>();
            _productRepo.Setup(x => x.List()).Returns(expectedProducts);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProducts);
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
            var productItems = new List<Product>
            {
                new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1500, 10)
            };

            var expectedProducts = new Products(productItems);
            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns(expectedProducts);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts(It.IsAny<string>());
            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProducts);
        }

        [Test]
        public void GivenProductNameNonExists_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.List(It.IsAny<string>())).Returns((Products)null);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts(It.IsAny<string>());
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
