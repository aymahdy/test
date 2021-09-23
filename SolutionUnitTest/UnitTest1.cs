using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Castle.Core.Internal;
using Solution;
using Xunit;
using NSubstitute;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SolutionUnitTest
{
    public class SolutionTests
    {

        private readonly IDataStore dataStore;
        private readonly ILogger<string> _logger;
        public SolutionTests()
        {
            _logger = Substitute.For<ILogger<string>>();
            dataStore = new DataStore(_logger);
        }
        [Fact]
        public void WhenCreate()
        {
            //Act
            var result = dataStore.Create("Key1", "Data1");
            //Assert
            result.Equals(true);
        }
        [Fact]
        public void WhenRead_ReturnsNull()
        {
            //Arrange
            string outResult;
            dataStore.Create("Key1", "data1");

            //Act
            var result = dataStore.TryRead("Key2", out outResult);

            //Assert
            outResult.IsNullOrEmpty();
        }


        [Fact]
        public void WhenRead_ReturnsNotNull()
        {
            //Arrange
            string outResult;
            dataStore.Create("Key1", "data1");

            //Act
            var result = dataStore.TryRead("Key1", out outResult);

            //Assert
            Assert.IsNotNull(outResult);
        }

        [Fact]
        public void WhenUpdate()
        {
            //Arrange
            dataStore.Create("Key1", "data1");

            //Act
            var result = dataStore.Update("Key1", "data2");

            //Assert
            result.Equals(true);
        }

        [Fact]
        public void WhenRemove_ReturnTrue()
        {
            //Arrange
            dataStore.Create("Key1", "data1");

            //Act
            var result = dataStore.Delete("Key1");

            //Assert
            result.Equals(true);
        }
    }
}
