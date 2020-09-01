using System;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;
using Moq;

using NUnit.Framework;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.OptionProcessor
{
    [TestFixture]
    public class UpdateOptionRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private UpdateOptionRequestProcessor _sut;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new UpdateOptionRequestProcessor(_productRepositoryMock.Object);
            _option = _fixture.Create<ProductOption>();
        }

        [Test]
        public async Task GivenValidInputs_ShouldUpdateProductOption()
        {
            // Arrange
            var request = _fixture.Create<UpdateProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(_option));

            // Act
            await _sut.UpdateProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), request);

            // Assert
            _productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ProductOption>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public async Task GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var request = _fixture.Build<UpdateProductOptionRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(_option));

            // Act 
            Func<Task> act = async () => { await _sut.UpdateProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public async Task GivenOptionIdNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            var request = _fixture.Create<UpdateProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult((ProductOption)null));

            // Act 
            Func<Task> act = async () => { await _sut.UpdateProductOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), request); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Product Option not found");
        }
    }
}
