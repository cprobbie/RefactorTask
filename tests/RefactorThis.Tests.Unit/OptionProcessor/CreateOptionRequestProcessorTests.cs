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
    public class CreateOptionRequestProcessorTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateOptionRequestProcessor _SUT;
        private ProductOption _option;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _SUT = new CreateOptionRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.Save(It.IsAny<ProductOption>()));
            _option = new ProductOption(Guid.NewGuid(), "32G", "32G storage");
        }

        [Test]
        public void GiveValidInputs_ShouldSaveProductOption()
        {
            // Act
            _SUT.CreateProductOption(It.IsAny<Guid>(), _option);
            // Assert
            _productRepositoryMock.Verify(x => x.Save(_option), Times.Once);
        }

        [TestCase("name", null)]
        [TestCase(" ", "some description")]
        public void GivenInvalidString_ShouldThrowArgumentException(string name, string description)
        {
            // Arrange
            var option = new ProductOption(Guid.NewGuid(), name, description);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProductOption(It.IsAny<Guid>(), option))
                .Should().Throw<ArgumentException>().WithMessage("Invalid input string");
        }

        [Test]
        public void GivenProductNonExists_ShouldThrowArgumentException()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Product)null);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProductOption(It.IsAny<Guid>(), _option))
                .Should().Throw<ArgumentException>().WithMessage("Product does not exist");
        }

        [Test]
        public void GivenOptionIdAlreadyExists_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new Product(Guid.NewGuid(), "iPad", "Apple tablet", 3000, 20);
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(product);
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), _option.Id)).Returns(_option);

            // Act and Assert
            _SUT.Invoking(x => x.CreateProductOption(It.IsAny<Guid>(), _option))
                .Should().Throw<ArgumentException>().WithMessage("Product Option Id already exists");
        }
    }
}
