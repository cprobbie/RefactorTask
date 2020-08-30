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
    public class GetProductsRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepo;

        [Test]
        public void ShouldReturnProductsResult()
        {
            // Arrange
            var expectedProducts = new Products
            {
                Items = new List<Product>
                {
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        DeliveryPrice = 10,
                        Price = 1000,
                        Name = "iPad",
                        Description = "Apple tablet"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        DeliveryPrice = 15,
                        Price = 2200,
                        Name = "MacBook",
                        Description = "Apple laptop"
                    }
                }
            };

            _productRepo = new Mock<IProductRepository>();
            _productRepo.Setup(x => x.List()).Returns(expectedProducts);
            var processor = new ListProductRequestProcessor(_productRepo.Object);

            // Act
            var result = processor.ListProducts();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProducts);
        }
    }
}
