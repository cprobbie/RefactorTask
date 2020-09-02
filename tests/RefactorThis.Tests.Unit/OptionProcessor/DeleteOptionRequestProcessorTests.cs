using System;
using System.Threading.Tasks;

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
        private DeleteOptionRequestProcessor _sut;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new DeleteOptionRequestProcessor(_productRepositoryMock.Object);
            _option = new ProductOption(Guid.NewGuid(), "32G", "32G storage");
        }

        [Test]
        public async Task GiveValidInputs_ShouldDeleteProductOption()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(_option));

            // Act
            await _sut.DeleteProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            _productRepositoryMock.Verify(x => x.DeleteOptionAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task GivenOptionIdNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), _option.Id)).Returns(Task.FromResult((ProductOption)null));

            // Act 
            Func<Task> act = async () => { await _sut.DeleteProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Product Option not exists");
        }
    }
}
