using System;

using FluentAssertions;
using Moq;

using NUnit.Framework;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.OptionProcessor
{
    [TestFixture]
    public class DeleteOptionRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private DeleteOptionRequestProcessor _SUT;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new DeleteOptionRequestProcessor(_productRepositoryMock.Object);
            _option = new ProductOption(Guid.NewGuid(), "32G", "32G storage");
        }

        [Test]
        public void GiveValidInputs_ShouldDeleteProductOption()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_option);

            // Act
            _SUT.DeleteProductOption(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            _productRepositoryMock.Verify(x => x.DeleteOption(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void GivenOptionIdNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), _option.Id)).Returns((ProductOption)null);

            // Act and Assert
            _SUT.Invoking(x => x.DeleteProductOption(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Should().Throw<ArgumentException>().WithMessage("Product Option not found");
        }
    }
}
