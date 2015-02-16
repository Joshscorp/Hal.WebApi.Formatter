using System;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest
{
    public class HalNodesTest
    {
        [Fact]
        public void Add_TwoHalNodesWithSameKeyTwice_ThrowsInvalidOperationException()
        {
            // Arrange
            HalNodes nodes = new HalNodes();
            HalNode nodeA = new HalNode("A", "value");
            HalNode nodeB = new HalNode("A", "value");

            // Act
            Action add = () =>
            {
                nodes.Add(nodeA);
                nodes.Add(nodeB);
            };

            // Assert
            Assert.Throws<InvalidOperationException>(() => add());
        }

        [Fact]
        public void Add_Node_Successfully()
        {
            // Arrange
            HalNodes nodes = new HalNodes();
            HalNode nodeA = new HalNode("A", "value");

            // Act
            nodes.Add(nodeA);

            // Assert
            Assert.Equal(1, nodes.Count);
        }

        [Fact]
        public void Add_NodeUsingKeyValue_Successfully()
        {
            // Arrange
            HalNodes nodes = new HalNodes();

            // Act
            nodes.Add("A", new HalLink("www.google.com"));

            // Assert
            Assert.Equal(1, nodes.Count);
        }
    }
}
