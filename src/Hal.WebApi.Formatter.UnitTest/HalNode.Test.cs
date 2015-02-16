using System;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest
{
    public class HalNodeTest
    {
        [Fact]
        public void Key_OnConstructionWithNullParamInput_ThrowsArgumentNullException()
        {
            // Arrange
            string key = null;

            // Act
            Action construction = () => new HalNode(key, null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => construction());
            Assert.Equal("key", exception.ParamName);
        }

        [Fact]
        public void Key_OnConstructionWithEmptyParamInput_ThrowsArgumentException()
        {
            // Arrange
            string key = string.Empty;

            // Act
            Action construction = () => new HalNode(key, null);

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => construction());
            Assert.Equal("key", exception.ParamName);
        }

        [Fact]
        public void Key_OnConstructionWithValidKeyParamInput_IsSetSuccessfully()
        {
            // Arrange
            string key = "somekey";

            // Act
            var node = new HalNode(key, null);

            // Assert            
            Assert.Equal(key, node.Key);
        }

        [Fact]
        public void Value_OnConstructionWithValidHrefParamInput_IsSetSuccessfully()
        {
            // Arrange
            string key = "somekey";
            Tuple<string, string> randomValue = new Tuple<string, string>("A", "B");

            // Act
            var node = new HalNode(key, randomValue);

            // Assert            
            Assert.Same(randomValue, node.Value);
        }
    }
}
