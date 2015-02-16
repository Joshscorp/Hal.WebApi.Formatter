using Hal.WebApi.Formatter.Json;
using Hal.WebApi.Formatter.UnitTest.Helper;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest.Json
{
    public class HalNodeConverterTest
    {
        [Fact]
        public void CanConvert_HalNode_ReturnsTrue()
        {
            // Arrange
            HalNodeConverter converter = new HalNodeConverter();

            // Act
            var result = converter.CanConvert(typeof(HalNode));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanConvert_HalNodeGenericType_ReturnsTrue()
        {
            // Arrange
            HalNodeConverter converter = new HalNodeConverter();

            // Act
            var result = converter.CanConvert(typeof(HalNode<string>));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void WriteJson_WithFullProperties_GeneratesCorrectJsonObject()
        {
            // Arrange
            HalNode node = new HalNode("currency", "USD");
            HalNodeConverter converter = new HalNodeConverter();
            StringWriter sw;
            JsonTextWriter writer;
            var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
            string expectedOutput = "\"currency\": \"USD\"";

            // Act
            converter.WriteJson(writer, node, serializer);

            // Assert
            Assert.Equal(expectedOutput, sw.ToString());
        }

        [Fact]
        public void WriteJson_WithNestedHalNodeValueObject_GeneratesCorrectJsonObject()
        {
            // Arrange
            HalNode nodeStatus = new HalNode("status", "Processing");
            HalNode node = new HalNode("currency", nodeStatus);

            HalNodeConverter converter = new HalNodeConverter();
            StringWriter sw;
            JsonTextWriter writer;
            var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
            serializer.Converters.Add(converter);
            string expectedOutput = "\"currency\": {\r\n  \"status\": \"Processing\"\r\n}";

            // Act
            converter.WriteJson(writer, node, serializer);

            // Assert
            // Verifies that start nested node has got { }, but not outer nodes
            Assert.Equal(expectedOutput, sw.ToString());
        }

        //[Fact]
        //public void WriteJson_WithNestedHalLinkValueObject_LinkConverterIsCalled()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        var expected = "\"currency\":";
        //        HalLink link = new HalLink("www.google.com");
        //        HalNode node = new HalNode("currency", link);
        //        bool isLinkConverterCalled = false;
        //        ShimHalLinkConverter linkConverter = new ShimHalLinkConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalLink).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                isLinkConverterCalled = true;
        //            }
        //        };
        //        HalNodeConverter converter = new HalNodeConverter();
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(linkConverter);

        //        // Act
        //        converter.WriteJson(writer, node, serializer);

        //        // Assert
        //        Assert.True(isLinkConverterCalled);
        //        Assert.Equal(expected, sw.ToString());
        //    }
        //}

        //[Fact]
        //public void WriteJson_WithNestedHalResourceValueObject_ResourceConverterIsCalled()
        //{
        //    using (ShimsContext.Create())
        //    {
        //        // Arrange
        //        var expected = "\"currency\":";
        //        HalResource resource = new HalResource(new HalLink("www.selflink.com"));
        //        HalNode node = new HalNode("currency", resource);
        //        bool isResourceConverterCalled = false;
        //        ShimHalResourceConverter resourceConverter = new ShimHalResourceConverter()
        //        {
        //            CanConvertType = (t) => typeof(HalResource).IsAssignableFrom(t),
        //            WriteJsonJsonWriterObjectJsonSerializer = (actualWriter, value, actualSerializer) =>
        //            {
        //                isResourceConverterCalled = true;
        //            }
        //        };
        //        HalNodeConverter converter = new HalNodeConverter();
        //        StringWriter sw;
        //        JsonTextWriter writer;
        //        var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
        //        serializer.Converters.Add(resourceConverter);

        //        // Act
        //        converter.WriteJson(writer, node, serializer);

        //        // Assert
        //        Assert.True(isResourceConverterCalled);
        //        Assert.Equal(expected, sw.ToString());
        //    }
        //}

        [Fact]
        public void ReadJson_WithStringReaderInputReadTwice_ReturnsStringSuccessfully()
        {
            // Arrange
            HalNodeConverter converter = new HalNodeConverter();
            string json = "{\"currency\": \"USD\"}";
            StringReader sr;
            JsonTextReader reader;
            JsonSerializer serializer = TestHelper.CreateReaderSerializer(json, out sr, out reader);
            reader.Read(); // To make sure it goes to the first character which is the startobject flag
            reader.Read(); // Read the second character

            // Act
            var result = converter.ReadJson(reader, typeof(HalNode), null, serializer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("currency", result); // Makes sure it is returning reader.value
        }

        [Fact]
        public void ReadJson_WithStringReaderInput_ReturnsNullByDefault()
        {
            // Arrange
            HalNodeConverter converter = new HalNodeConverter();
            string json = "{\"currency\": \"USD\"}";
            StringReader sr;
            JsonTextReader reader;
            JsonSerializer serializer = TestHelper.CreateReaderSerializer(json, out sr, out reader);

            // Act
            var result = converter.ReadJson(reader, typeof(HalNode), null, serializer);

            // Assert
            Assert.Null(result);
        }
    }
}
