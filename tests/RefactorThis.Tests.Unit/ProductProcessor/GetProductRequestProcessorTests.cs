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
    public class GetProductRequestProcessorTests_GetProductById
    {
        private Mock<IProductRepository> _productRepo;

        [SetUp]
        public void Setup()
        {
            _productRepo = new Mock<IProductRepository>();
        }

        [Test]
        public void GivenValidId_ShouldReturnProduct()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedProduct = new Product(id, "iPad", "Apple tablet", 800, 15);
            var queryResult = new Product(id, "iPad", "Apple tablet", 800, 15);
            _productRepo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(queryResult);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.GetProductById(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProduct);
        }

        [Test]
        public void GivenIdNonExists_ShouldReturnNull()
        {
            // Arrange
            _productRepo.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);
            var SUT = new GetProductRequestProcessor(_productRepo.Object);

            // Act
            var result = SUT.GetProductById(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
        }
    }
}
