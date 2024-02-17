using System;
using System.Threading.Tasks;

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
    public class ProductServiceTests
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
            _productRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task GiveValidInputs_ShouldSaveProduct()
        {
            // Arrange
            var request = _fixture.Create<CreateProductRequest>();
            
            // Act
            await _sut.CreateProductAsync(request);

            // Assert
            _productRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<Product>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public async Task GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var request = _fixture.Build<CreateProductRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            // Act 
            Func<Task> act = async () => { await _sut.CreateProductAsync(request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input string");
        }

        [TestCase(-1, 199)]
        [TestCase(100, -10)]
        public async Task GivenInvalidAmount_ShouldThrowArgumentException(decimal price, decimal deliveryPrice)
        {
            // Arrange
            var request = _fixture.Build<CreateProductRequest>()
                .With(x => x.Price, price)
                .With(x => x.DeliveryPrice, deliveryPrice)
                .Create();

            // Act 
            Func<Task> act = async () => { await _sut.CreateProductAsync(request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input amount");
        }
    }
}
