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
    public class CreateOptionRequestProcessorTests
    {
        private Fixture _fixture;
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateOptionRequestProcessor _SUT;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new CreateOptionRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<ProductOption>()));
            _product = _fixture.Create<Product>(); ;
        }

        [Test]
        public void GiveValidInputs_ShouldSaveProductOption()
        {
            // Arrange
            var optionRequest = _fixture.Create<ProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(_product);

            // Act
            _SUT.CreateProductOption(It.IsAny<Guid>(), optionRequest);
            // Assert
            _productRepositoryMock.Verify(x => x.Save(It.IsAny<ProductOption>()), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var optionRequest = _fixture.Build<ProductOptionRequest>()
                .With(x => x.Name, name)
                .With(x => x.Description, description)
                .Create();

            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(_product);
            var option = new ProductOption(Guid.NewGuid(), name, description);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProductOption(It.IsAny<Guid>(), optionRequest))
                .Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public void GivenProductNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            var optionRequest = _fixture.Create<ProductOptionRequest>();
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProductOption(It.IsAny<Guid>(), optionRequest))
                .Should().Throw<ArgumentException>().WithMessage("Product does not exist");
        }
    }
}
