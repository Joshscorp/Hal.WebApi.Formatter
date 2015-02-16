using System;
using System.Collections.Generic;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest
{
    public class HalReservedLinksTest
    {
        [Fact]
        public void Self_OnConstructionWithNullParam_ThrowsArgumentNullException()
        {
            // Arrange
            HalLink self = null;

            // Act
            Action construction = () => new HalReservedLinks(self);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => construction());
            Assert.Equal("selfLink", exception.ParamName);
        }

        [Fact]
        public void Self_OnConstruction_IsAddedSuccessfully()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);

            // Act
            var links = new HalReservedLinks(selfLink);

            // Assert
            Assert.Same(selfLink, links.GetSelfNode().Value);
        }

        [Fact]
        public void Curies_OnConstructionWithCuriesParam_IsAddedSuccessfully()
        {
            // Arrange
            var href = "www.selflink.com";
            List<HalLinkCurie> curiesLink = new List<HalLinkCurie>()
                {
                   new HalLinkCurie("www.google.com", "gn"),
                   new HalLinkCurie("www.microsoft.com", "gn"),
                   new HalLinkCurie("www.abc.com", "gn")
                };
            HalLink selfLink = new HalLink(href);

            // Act
            var links = new HalReservedLinks(selfLink, curiesLink);

            // Assert
            Assert.Same(curiesLink, links.GetCuriesNode().Value);
        }

        [Fact]
        public void Curies_OnConstructionWithNoCuriesParam_IsEmptyCollectionCreatedSuccessfully()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);

            // Act
            var links = new HalReservedLinks(selfLink);

            // Assert
            Assert.NotNull(links.GetCuriesNode().Value);
            Assert.IsType<List<HalLinkCurie>>(links.GetCuriesNode().Value);
        }

        [Fact]
        public void Add_StringSelfAsKeyParam_ThrowsInvalidArgumentException()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);
            string key = "self";
            string value = null;
            var links = new HalReservedLinks(selfLink);

            // Act
            Action add = () => links.Add(key, value);

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => add());
            Assert.Equal("key", exception.ParamName);
        }

        [Fact]
        public void Add_StringCuriesAsKeyParam_ThrowsInvalidArgumentException()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);
            string key = "curies";
            string value = null;
            var links = new HalReservedLinks(selfLink);

            // Act
            Action add = () => links.Add(key, value);

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => add());
            Assert.Equal("key", exception.ParamName);
        }

        [Fact]
        public void Add_SameStringTwice_ThrowsInvalidOperationException()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);
            var links = new HalReservedLinks(selfLink);

            // Act
            Action add = () =>
            {
                links.Add("name", "value");
                links.Add("name", "value");
            };

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => add());
        }

        [Fact]
        public void Add_StringParam_Successfully()
        {
            // Arrange
            var href = "www.selflink.com";
            HalLink selfLink = new HalLink(href);
            string key = "someKey";
            string value = "someValue";
            var links = new HalReservedLinks(selfLink);

            // Act
            links.Add(key, value);

            // Assert            
            Assert.Equal(value, links[key].Value);
        }
    }
}
