using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.OptionProcessor
{
    [TestFixture]
    public class GetOptionsRequestProcessorTests_ListOptions
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
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var options = new List<ProductOption>
            {
                new ProductOption(id1, "32G", "32G storage"),
                new ProductOption(id2, "64G", "64G storage")
            };

            var expectedDto = new ProductOptionsDto(options);
            
            var queryResult = new List<ProductOption>
            {
                new ProductOption(id1, "32G", "32G storage"),
                new ProductOption(id2, "64G", "64G storage")
            };
            

            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns(queryResult);

            // Act
            var result = sut.ListOptions(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void GivenProductOptionsListNull_ShouldReturnNull()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns((IList<ProductOption>)null);

            // Act and Assert
            sut.Invoking(x => x.ListOptions(It.IsAny<Guid>())).Should().Throw<KeyNotFoundException>().WithMessage("There is no option available");
        }
        [Test]
        public void GivenProductOptionsListEmpty_ShouldReturnNull()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns(new List<ProductOption>());

            // Act and Assert
            sut.Invoking(x=> x.ListOptions(It.IsAny<Guid>())).Should().Throw<KeyNotFoundException>().WithMessage("There is no option available");
        }
    }

    [TestFixture]
    public class GetOptionsRequestProcessorTests_GetOption
    {
        private Mock<IProductRepository> _productRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
        }

        [Test]
        public void GivenProductOptionExist_ShouldReturnExpectedOptionResult()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedOption = new ProductOption(id, "32G", "32G storage");
            var queryResult = new ProductOption(id, "32G", "32G storage");


            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(queryResult);

            // Act
            var result = sut.GetOptionById(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedOption);
        }

        [Test]
        public void GivenProductOptionNonExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((ProductOption)null);

            // Act and Assert
            sut.Invoking(x => x.GetOptionById(It.IsAny<Guid>(), It.IsAny<Guid>())).Should().Throw<KeyNotFoundException>().WithMessage("Option not found");
        }
    }
}
