using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest
{
    public class HalReservedEmbeddedResourcesTest
    {
        [Fact]
        public void Add_SinglePropertyWithMultiResources_Successfully()
        {
            // Arrange
            HalReservedEmbeddedResources resources = new HalReservedEmbeddedResources();
            string key = "profile";
            IEnumerable<HalResource> profileResources = new List<HalResource>()
                {
                    new HalResource(new HalLink("www.selflink.com")),
                    new HalResource(new HalLink("www.selflink.com")),
                };

            // Act
            resources.Add(key, profileResources);

            // Assert
            Assert.Equal(1, resources.Count());
            Assert.Equal(2, resources.First().Value.Count());
        }

        [Fact]
        public void Add_MultiPropertiesWithMultiResources_Successfully()
        {
            // Arrange
            HalReservedEmbeddedResources resources = new HalReservedEmbeddedResources();
            string profileKey = "profile";
            IEnumerable<HalResource> profileResources = new List<HalResource>()
                {
                    new HalResource(new HalLink("www.selflink.com")),
                    new HalResource(new HalLink("www.selflink.com")),
                };
            string detailKey = "detail";
            IEnumerable<HalResource> detailResources = new List<HalResource>()
                {
                    new HalResource(new HalLink("www.selflink.com")),
                    new HalResource(new HalLink("www.selflink.com")),
                };

            // Act
            resources.Add(profileKey, profileResources);
            resources.Add(detailKey, detailResources);

            // Assert
            Assert.Equal(2, resources.Count());
            Assert.Equal(profileResources, resources[profileKey].Value);
            Assert.Equal(detailResources, resources[detailKey].Value);
        }
    }
}
