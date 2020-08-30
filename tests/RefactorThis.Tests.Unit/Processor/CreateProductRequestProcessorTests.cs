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
            var request = new Product
            {
                Id = Guid.NewGuid(),
                DeliveryPrice = 10,
                Price = 1000,
                Name = "iPad",
                Description = "Apple tablet"
            };

            _productRepositoryMock = new Mock<IProductRepository>();
            var SUT = new CreateProductRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<Product>()));
            // Act
            SUT.CreateProduct(request);

            // Assert
            _productRepositoryMock.Verify(x => x.Save(request), Times.Once);
        }
    }
}
