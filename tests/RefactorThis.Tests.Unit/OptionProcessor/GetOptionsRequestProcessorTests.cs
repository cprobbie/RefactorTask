using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task GivenProductOptionsExist_ShouldReturnExpectedOptionsResult()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var options = new List<Option>
            {
                new Option(id1, "32G", "32G storage"),
                new Option(id2, "64G", "64G storage")
            };

            var expectedDto = new ProductOptionsDto(options);
            
            var queryList = new List<Option>
            {
                new Option(id1, "32G", "32G storage"),
                new Option(id2, "64G", "64G storage")
            };
            IList<Option> queryResult = queryList;

            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptionsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(queryResult));

            // Act
            var result = await sut.ListOptionsAsync(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public async Task GivenProductOptionsListNull_ShouldReturnKeyNotFoundException()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptionsAsync(It.IsAny<Guid>())).Returns(Task.FromResult((IList<Option>)null));

            // Act 
            Func<Task> act = async () => { await sut.ListOptionsAsync(It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("There is no option available");
        }

        [Test]
        public async Task GivenProductOptionsListEmpty_ShouldReturnKeyNotFoundException()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            var optionList = new List<Option>();
            IList<Option> queryResult = optionList;
            _productRepositoryMock.Setup(x => x.ListOptionsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(queryResult));

            // Act 
            Func<Task> act = async () => { await sut.ListOptionsAsync(It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("There is no option available");
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
        public async Task GivenProductOptionExist_ShouldReturnExpectedOptionResult()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedOption = new Option(id, "32G", "32G storage");
            var queryResult = new Option(id, "32G", "32G storage");


            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(queryResult));

            // Act
            var result = await sut.GetOptionByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedOption);
        }

        [Test]
        public async Task GivenProductOptionNonExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var sut = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOptionAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult((Option)null));

            // Act 
            Func<Task> act = async () => { await sut.GetOptionByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>()); };

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Option not found");
        }
    }
}
