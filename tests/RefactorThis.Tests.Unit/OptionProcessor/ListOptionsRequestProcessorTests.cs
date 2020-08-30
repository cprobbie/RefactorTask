using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.Processor
{
    [TestFixture]
    public class ListOptionsRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
        }

        [Test]
        public void GivenProductOptionsExist_ShouldReturnExpectedOptionsResult()
        {
            // Arrange
            var expectedOptionItems = new List<ProductOption>
            {
                new ProductOption(Guid.NewGuid(), "32G", "32G storage"),
                new ProductOption(Guid.NewGuid(), "64G", "64G storage")
            };
            var expectedOptions = new ProductOptions(expectedOptionItems);

            var queryResultItem = new List<ProductOption>
            {
                new ProductOption(Guid.NewGuid(), "32G", "32G storage"),
                new ProductOption(Guid.NewGuid(), "64G", "64G storage")
            };
            var queryResult = new ProductOptions(queryResultItem);

            var SUT = new ListOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns(queryResult);

            // Act
            var result = SUT.ListOptions(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedOptions);
        }

        [Test]
        public void GivenProductOptionsNonExist_ShouldReturnNull()
        {
            // Arrange
            var SUT = new ListOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns((ProductOptions)null);
            // Act
            var result = SUT.ListOptions(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
        }
    }
}
