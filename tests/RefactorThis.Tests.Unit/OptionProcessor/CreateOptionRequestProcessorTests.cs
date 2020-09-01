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
    public class CreateOptionRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateOptionRequestProcessor _sut;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new CreateOptionRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<ProductOption>())).Returns(Task.CompletedTask);
            _product = _fixture.Create<Product>();
        }

        [Test]
        public async Task GiveValidInputs_ShouldSaveProductOption()
        {
            // Arrange
            var optionRequest = _fixture.Create<CreateProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_product));

            // Act
            await _sut.CreateProductOptionAsync(It.IsAny<Guid>(), optionRequest);

            // Assert
            _productRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<ProductOption>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public async Task GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var optionRequest = _fixture.Build<CreateProductOptionRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(_product));

            // Act 
            Func<Task> act = async () => { await _sut.CreateProductOptionAsync(It.IsAny<Guid>(), optionRequest); };
            
            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public async Task GivenProductNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            var optionRequest = _fixture.Create<CreateProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Product)null));

            // Act 
            Func<Task> act = async () => { await _sut.CreateProductOptionAsync(It.IsAny<Guid>(), optionRequest); };

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Product does not exist");
        }
    }
}
