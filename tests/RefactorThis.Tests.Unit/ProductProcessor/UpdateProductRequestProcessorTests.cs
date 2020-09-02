using System;
using System.Collections.Generic;
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
    public class UpdateProductRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateProductRequestProcessor _sut;
        private Product _queryResult;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new UpdateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _queryResult = _fixture.Create<Product>();
        }

        [Test]
        public async Task GiveValidInputs_ShouldUpdateProduct()
        {
            // Arrange
            var request = _fixture.Create<UpdateProductRequest>();
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_queryResult));

            // Act
            await _sut.UpdateProductAsync(It.IsAny<Guid>(), request);

            // Assert
            _productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public async Task GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var request = _fixture.Build<UpdateProductRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_queryResult));

            // Act 
            Func<Task> act = async () => { await _sut.UpdateProductAsync(It.IsAny<Guid>(), request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input string");
        }

        [TestCase(-1, 199)]
        [TestCase(100, -10)]
        public async Task GivenInvalidAmount_ShouldThrowArgumentException(decimal price, decimal deliveryPrice)
        {
            // Arrange
            var request = _fixture.Build<UpdateProductRequest>()
                .With(x => x.Price, price)
                .With(x => x.DeliveryPrice, deliveryPrice)
                .Create();

            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_queryResult));

            // Act 
            Func<Task> act = async () => { await _sut.UpdateProductAsync(It.IsAny<Guid>(), request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input amount");
        }

        [Test]
        public async Task GivenProductNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var request = _fixture.Create<UpdateProductRequest>();
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Product)null));

            // Act 
            Func<Task> act = async () => { await _sut.UpdateProductAsync(It.IsAny<Guid>(), request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Product not exists, update failed");
        }
    }
}
