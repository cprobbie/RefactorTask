﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;
using RefactorThis.Core.OptionProcessor;

namespace RefactorThis.Core.Unit.Processor
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
            var expectedOptionItems = new List<ProductOption>
            {
                new ProductOption(id1, "32G", "32G storage"),
                new ProductOption(id2, "64G", "64G storage")
            };
            var expectedOptions = new ProductOptions(expectedOptionItems);

            var queryResultItem = new List<ProductOption>
            {
                new ProductOption(id1, "32G", "32G storage"),
                new ProductOption(id2, "64G", "64G storage")
            };
            var queryResult = new ProductOptions(queryResultItem);

            var SUT = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns(queryResult);

            // Act
            var result = SUT.ListOptions(It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedOptions);
        }

        [Test]
        public void GivenProductOptionsNonExist_ShouldReturnNull()
        {
            // Arrange
            var SUT = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.ListOptions(It.IsAny<Guid>())).Returns((ProductOptions)null);
            // Act
            var result = SUT.ListOptions(It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
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


            var SUT = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(queryResult);

            // Act
            var result = SUT.GetOptionById(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedOption);
        }

        [Test]
        public void GivenProductOptionsNonExist_ShouldReturnNull()
        {
            // Arrange
            var SUT = new GetOptionsRequestProcessor(_productRepositoryMock.Object);
            _productRepositoryMock.Setup(x => x.GetOption(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns((ProductOption)null);

            // Act
            var result = SUT.GetOptionById(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            result.Should().BeNull();
        }
    }
}