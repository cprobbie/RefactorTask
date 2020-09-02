using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.ProductProcessor;

namespace RefactorThis.Core.Unit.ProductProcessor
{
    [TestFixture]
    public class DeleteProductRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private DeleteProductRequestProcessor _sut;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new DeleteProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.DeleteProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        }

        [Test]
        public async Task GivenValidId_ShouldDeleteProduct()
        {
            // Arrange
            var queryResult = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1000, 100);
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(queryResult));

            // Act
            await _sut.DeleteProductAsync(It.IsAny<Guid>());

            // Assert
            _productRepositoryMock.Verify(x => x.DeleteProductAsync(queryResult), Times.Once);
        }

        [Test]
        public async Task GivenProductNotExists_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Product)null));

            // Act 
            Func<Task> act = async () => { await _sut.DeleteProductAsync(It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Product not exists");
        }
    }
}
