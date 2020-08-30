using System;
using System.Collections.Generic;
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
    public class UpdateProductRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateProductRequestProcessor _SUT;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new UpdateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()));
        }

        [TestCase(0)]
        [TestCase(100)]
        public void GiveValidInputs_ShouldUpdateProduct(decimal deliveryPrice)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1500, deliveryPrice);
            var existingProduct = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1000, deliveryPrice);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(existingProduct);

            // Act
            _SUT.UpdateProduct(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(product), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), name, description, 1500, 10);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(It.IsAny<Product>);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProduct(product)).Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [TestCase(-1, 199)]
        [TestCase(100, -10)]
        public void GivenInvalidAmount_ShouldThrowArgumentException(decimal price, decimal deliveryPrice)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", price, deliveryPrice);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(It.IsAny<Product>);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProduct(product)).Should().Throw<ArgumentException>().WithMessage("Invalid input amount");
        }

        [Test]
        public void GivenProductNotExist_ShouldThrowException()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1000, 10);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProduct(product)).Should().Throw<KeyNotFoundException>().WithMessage("Product not found");
        }
    }
}
