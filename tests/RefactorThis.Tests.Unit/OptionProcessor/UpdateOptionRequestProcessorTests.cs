using System;
using System.Collections.Generic;
using System.Text;

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
        private UpdateOptionRequestProcessor _SUT;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new UpdateOptionRequestProcessor(_productRepositoryMock.Object);
            _option = _fixture.Create<ProductOption>();
        }

        [Test]
        public void GiveValidInputs_ShouldUpdateProductOption()
        {
            // Arrange
            var request = _fixture.Create<ProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_option);

            // Act
            _SUT.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), request);

            // Assert
            _productRepositoryMock.Verify(x => x.Update(It.IsAny<ProductOption>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var request = _fixture.Build<ProductOptionRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_option);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), request))
                .Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public void GivenOptionIdNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            var request = _fixture.Create<ProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((ProductOption)null);

            // Act and Assert
            _SUT.Invoking(x => x.UpdateProductOption(It.IsAny<Guid>(), It.IsAny<Guid>(), request))
                .Should().Throw<ArgumentException>().WithMessage("Product Option not found");
        }
    }
}
