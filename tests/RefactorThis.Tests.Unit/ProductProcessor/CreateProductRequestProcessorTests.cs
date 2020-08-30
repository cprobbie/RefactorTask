using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Processor;

namespace RefactorThis.Core.Unit.Processor
{
    [TestFixture]
    public class CreateProductRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateProductRequestProcessor _SUT;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new CreateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<Product>()));
        }

        [TestCase(0)]
        [TestCase(100)]
        public void GiveValidInputs_ShouldSaveProduct(decimal deliveryPrice)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1500, deliveryPrice);

            // Act
            _SUT.CreateProduct(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Save(product), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), name, description, 1500, 10);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProduct(product)).Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [TestCase(-1, 199)]
        [TestCase(100, -10)]
        public void GivenInvalidAmount_ShouldThrowArgumentException(decimal price, decimal deliveryPrice)
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", price, deliveryPrice);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProduct(product)).Should().Throw<ArgumentException>().WithMessage("Invalid input amount");
        }

        [Test]
        public void GivenIdAlreadyExists_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 2000, 10);
            _productRepositoryMock.Setup(x => x.Get(product.Id)).Returns(product);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProduct(product)).Should().Throw<ArgumentException>().WithMessage("Product Id already exists");
        }
    }
}
