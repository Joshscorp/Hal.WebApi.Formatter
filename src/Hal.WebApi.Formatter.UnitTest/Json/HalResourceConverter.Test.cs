using Hal.WebApi.Formatter.Json;
using Hal.WebApi.Formatter.UnitTest.Helper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest.Json
{
    public class HalResourceConverterTest
    {
        [Fact]
        public void CanConvert_HalResourceType_ReturnsTrue()
        {
            // Arrange
            HalResourceConverter converter = new HalResourceConverter();

            // Act
            var result = converter.CanConvert(typeof(HalResource));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ReadJson_WithStringReaderInputReadTwice_ReturnsStringSuccessfully()
        {
            // Arrange
            HalResourceConverter converter = new HalResourceConverter();
            string json = "{\"currency\": \"USD\"}";
            StringReader sr;
            JsonTextReader reader;
            JsonSerializer serializer = TestHelper.CreateReaderSerializer(json, out sr, out reader);
            reader.Read(); // To make sure it goes to the first character which is the startobject flag
            reader.Read(); // Read the second character

            // Act
            var result = converter.ReadJson(reader, typeof(HalResource), null, serializer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("currency", result); // Makes sure it is returning reader.value
        }

        [Fact]
        public void ReadJson_WithStringReaderInput_ReturnsNullByDefault()
        {
            // Arrange
            HalResourceConverter converter = new HalResourceConverter();
            string json = "{\"currency\": \"USD\"}";
            StringReader sr;
            JsonTextReader reader;
            JsonSerializer serializer = TestHelper.CreateReaderSerializer(json, out sr, out reader);

            // Act
            var result = converter.ReadJson(reader, typeof(HalResource), null, serializer);

            // Assert
            Assert.Null(result);
        }

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksAndContent_SuccessfullyInvokesNodeConverterAndLinkConverter()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        HalLink link = new HalLink("http://www.google.com");
        //        bool isNodeConverterCalled = false;
        //        bool isLinkConverterCalled = false;
        //        HalResource resource = new HalResource(link);
        //        resource.Content.Add("name", "random");
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                isNodeConverterCalled = true;
        //            }
        //        };
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                isLinkConverterCalled = true;
        //            }
        //        };

        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.True(isNodeConverterCalled);
        //        Assert.True(isLinkConverterCalled);
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksAndTwoContent_SuccessfullyCalledNodeConverterTwice()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        int nodeConverterCalledCount = 0;
        //        HalResource resource = new HalResource(link);
        //        resource.Content.Add("name", "random");
        //        resource.Content.Add("property", "random");
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                nodeConverterCalledCount++;
        //            }
        //        };
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                actualWriter.WriteStartObject();
        //                actualWriter.WriteEndObject();
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.Equal(2, nodeConverterCalledCount);
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksContainingCuries_SuccessfullyCreatedCuriesArray()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        List<HalLinkCurie> curies = new List<HalLinkCurie>()
        //            {
        //                new HalLinkCurie("www.curie.com", "gn"),
        //                new HalLinkCurie("www.curie.com", "gn2"),
        //            };
        //        string expectedCuries = "\"curies\": [";
        //        bool isCurieLinkConverterCalled = false;
        //        HalResource resource = new HalResource(link, curies);
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                // Do nothing
        //            }
        //        };
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                // Write dummy object tags
        //                actualWriter.WriteStartObject();
        //                if (value is HalLink)
        //                {
        //                    var actualLink = value as HalLink;
        //                    if (actualLink.Href.Contains("www.curie.com"))
        //                        isCurieLinkConverterCalled = true;
        //                }
        //                actualWriter.WriteEndObject();
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.Contains(expectedCuries, sw.ToString());
        //        Assert.True(isCurieLinkConverterCalled);
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksWithoutCuries_DoesNotCreateCuries()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        string expectedCuries = "\"curies\": [";
        //        HalResource resource = new HalResource(link);
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                // Do nothing
        //            }
        //        };
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                actualWriter.WriteStartObject();
        //                actualWriter.WriteEndObject();
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.DoesNotContain(expectedCuries, sw.ToString());
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksOnly_SuccessfullyInvokesLinkConverterButNotNodeConverter()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        bool isNodeConverterCalled = false;
        //        bool isLinkConverterCalled = false;
        //        int linkCount = 0;
        //        int nodeCount = 0;

        //        HalResource resource = new HalResource(link);
        //        HalNode nodeA = new HalNode("otherA", new HalLink("http://www.otherA.com"));
        //        resource.Links.Add(nodeA);
        //        HalNode nodeB = new HalNode("otherB", new HalLink("http://www.otherB.com"));
        //        resource.Links.Add(nodeB);

        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                nodeCount++;
        //                isNodeConverterCalled = true;
        //            }
        //        };
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                actualWriter.WriteStartObject();
        //                actualWriter.WriteEndObject();
        //                linkCount++;
        //                isLinkConverterCalled = true;
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.False(isNodeConverterCalled);
        //        Assert.True(isLinkConverterCalled);
        //        Assert.True(nodeCount == 0);
        //        Assert.True(linkCount == 3);
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksAndMultiEmbeddedResources_SuccessfullyCreatedEmbeddedArray()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        HalResource resource = new HalResource(link);
        //        resource.Embedded.Add("order", new List<HalResource>()
        //                    {
        //                        new HalResource(new HalLink("http://www.orderlink1.com")), 
        //                        new HalResource(new HalLink("http://www.orderlink2.com"))
        //                    });
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        bool isOrderLink1Called = false;
        //        bool isOrderLink2Called = false;
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                actualWriter.WriteStartObject();
        //                if (value is HalLink)
        //                {
        //                    var actualLink = value as HalLink;
        //                    if (actualLink.Href.Contains("www.orderlink1.com"))
        //                        isOrderLink1Called = true;
        //                    else if (actualLink.Href.Contains("www.orderlink2.com"))
        //                        isOrderLink2Called = true;
        //                }
        //                actualWriter.WriteEndObject();
        //            }
        //        };
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                // Do Nothing
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(resourceConverter);
        //        string embeddedOutput = "\"_embedded\": {";
        //        string orderOutput = "\"order\": [";

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.Contains(embeddedOutput, sw.ToString());
        //        Assert.Contains(orderOutput, sw.ToString());
        //        Assert.True(isOrderLink1Called);
        //        Assert.True(isOrderLink2Called);
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithValidResourceObjectWithLinksAndSingleEmbeddedResources_SuccessfullyCreatedEmbeddedArray()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        HalLink link = new HalLink("http://www.google.com");
        //        HalResource resource = new HalResource(link);
        //        resource.Embedded.Add("order", new List<HalResource>()
        //                    {
        //                        new HalResource(new HalLink("http://www.orderlink1.com"))
        //                    });
        //        HalResourceConverter resourceConverter = new HalResourceConverter();
        //        bool isOrderLink1Called = false;
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                actualWriter.WriteStartObject();
        //                if (value is HalLink)
        //                {
        //                    var actualLink = value as HalLink;
        //                    if (actualLink.Href.Contains("www.orderlink1.com"))
        //                        isOrderLink1Called = true;
        //                }
        //                actualWriter.WriteEndObject();
        //            }
        //        };
        //        ShimHalNodeConverter nodeConverter = new ShimHalNodeConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalNode).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                // Do Nothing
        //            }
        //        };
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(linkConverter);
        //        serializer.Converters.Add(nodeConverter);
        //        serializer.Converters.Add(resourceConverter);
        //        string embeddedOutput = "\"_embedded\": {";
        //        string orderOutput = "\"order\": {";

        //        // Act
        //        resourceConverter.WriteJson(writer, resource, serializer);

        //        // Assert
        //        Assert.Contains(embeddedOutput, sw.ToString());
        //        Assert.Contains(orderOutput, sw.ToString());
        //        Assert.True(isOrderLink1Called);
        //    }
        //}
    }
}
