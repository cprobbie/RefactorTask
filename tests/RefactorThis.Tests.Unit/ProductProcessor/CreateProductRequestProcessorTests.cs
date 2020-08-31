using System;
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
    public class CreateProductRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateProductRequestProcessor _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new CreateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<Product>()));
        }

        [Test]
        public void GiveValidInputs_ShouldSaveProduct()
        {
            // Arrange
            var request = _fixture.Create<ProductRequest>();
            
            // Act
            _sut.CreateProduct(request);

            // Assert
            _productRepositoryMock.Verify(x => x.Save(It.IsAny<Product>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var request = _fixture.Build<ProductRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();
            

            // Act and Assert
            _sut.Invoking(x => x.CreateProduct(request)).Should().Throw<ArgumentException>().WithMessage("Invalid input string");
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

            // Act and Assert
            _sut.Invoking(x => x.CreateProduct(request)).Should().Throw<ArgumentException>().WithMessage("Invalid input amount");
        }
    }
}
