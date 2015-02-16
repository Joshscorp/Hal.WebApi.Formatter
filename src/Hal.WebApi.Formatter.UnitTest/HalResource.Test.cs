using Xunit;

namespace Hal.WebApi.Formatter.UnitTest
{
    public class ResourceTest
    {
        [Fact]
        public void ReservedLinks_OnConstructionWithSelfLink_CreatedSuccessfully()
        {
            // Arrange
            HalLink selfLink = new HalLink("www.google.com.au");

            // Act
            HalResource resource = new HalResource(selfLink);

            // Assert
            Assert.Equal(selfLink, resource.Links["self"].Value);
        }
    }
}
