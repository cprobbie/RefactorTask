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
    public class CreateProductRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;

        [Test]
        public void ShouldSaveProduct()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 1500, 10);

            _productRepositoryMock = new Mock<IProductRepository>();
            var SUT = new CreateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<Product>()));
            // Act
            SUT.CreateProduct(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Save(product), Times.Once);
        }
    }
}
