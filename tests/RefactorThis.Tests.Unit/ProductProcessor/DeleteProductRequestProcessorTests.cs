using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.Processor;

namespace RefactorThis.Core.Unit.Processor
{
    [TestFixture]
    public class DeleteProductRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private DeleteProductRequestProcessor _SUT;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new DeleteProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<Guid>()));
        }

        [Test]
        public void GiveValidId_ShouldDeleteProduct()
        {
            // Arrange
            var queryResult = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1000, 100);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(queryResult);

            // Act
            _SUT.DeleteProduct(It.IsAny<Guid>());

            // Assert
            _productRepositoryMock.Verify(x => x.DeleteProduct(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void GivenProductNotExists_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);

            // Act and Assert
            _SUT.Invoking(x => x.DeleteProduct(It.IsAny<Guid>())).Should().Throw<KeyNotFoundException>().WithMessage("Product not found");
        }
    }
}
