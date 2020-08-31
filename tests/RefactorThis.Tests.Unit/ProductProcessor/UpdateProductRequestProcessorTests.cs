using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.ProductProcessor;

namespace RefactorThis.Core.Unit.ProductProcessor
{
    [TestFixture]
    public class UpdateProductRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateProductRequestProcessor _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new UpdateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()));
        }

        [Test]
        public void GiveValidInputs_ShouldUpdateProduct()
        {
            // Arrange
            var request = _fixture.Create<ProductRequest>();
            var queryResult = _fixture.Create<Product>();
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(queryResult);

            // Act
            _sut.UpdateProduct(It.IsAny<Guid>(), request);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var product = _fixture.Build<ProductRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(It.IsAny<Product>);

            // Act and Assert
            _sut.Invoking(x => x.UpdateProduct(It.IsAny<Guid>(), product)).Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [TestCase(-1, 199)]
        [TestCase(100, -10)]
        public void GivenInvalidAmount_ShouldThrowArgumentException(decimal price, decimal deliveryPrice)
        {
            // Arrange
            var request = _fixture.Build<ProductRequest>()
                .With(x => x.Price, price)
                .With(x => x.DeliveryPrice, deliveryPrice)
                .Create();

            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(It.IsAny<Product>);

            // Act and Assert
            _sut.Invoking(x => x.UpdateProduct(It.IsAny<Guid>(), request)).Should().Throw<ArgumentException>().WithMessage("Invalid input amount");
        }

        [Test]
        public void GivenProductNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = _fixture.Create<ProductRequest>();
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);

            // Act and Assert
            _sut.Invoking(x => x.UpdateProduct(It.IsAny<Guid>(), request)).Should().Throw<KeyNotFoundException>().WithMessage("Product not found");
        }
    }
}
