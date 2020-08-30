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
            var expectedProducts = new Products
            {
                Items = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        DeliveryPrice = 10,
                        Price = 1000,
                        Name = "iPad",
                        Description = "Apple tablet"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        DeliveryPrice = 15,
                        Price = 2200,
                        Name = "MacBook",
                        Description = "Apple laptop"
                    }
                }
            };

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
            var name = "iPad";
            var expectedProducts = new Products
            {
                Items = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        DeliveryPrice = 10,
                        Price = 1000,
                        Name = "iPad",
                        Description = "Apple tablet"
                    }
                }
            };
            
            _productRepo.Setup(x => x.List(name)).Returns(expectedProducts);
            var SUT = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.ListProducts(name);
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
