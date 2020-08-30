using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;
using Moq;

using NUnit.Framework;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.OptionProcessor
{
    [TestFixture]
    public class UpdateOptionRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateOptionRequestProcessor _SUT;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new UpdateOptionRequestProcessor(_productRepositoryMock.Object);
            _option = new ProductOption(Guid.NewGuid(), "32G", "32G storage");
        }

        [Test]
        public void GiveValidInputs_ShouldUpdateProductOption()
        {
            // Act
            _SUT.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), _option);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(_option), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var option = new ProductOption(Guid.NewGuid(), name, description);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), option))
                .Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public void GivenOptionIdNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), _option.Id)).Returns((ProductOption)null);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), _option))
                .Should().Throw<ArgumentException>().WithMessage("Product Option not found");
        }
    }
}
