using Hal.WebApi.Formatter.Json;
using Hal.WebApi.Formatter.UnitTest.Helper;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace Hal.WebApi.Formatter.UnitTest.Json
{
    public class HalLinkConverterTest
    {
        [Fact]
        public void WriteJson_WithFullProperties_GeneratesCorrectJsonObject()
        {
            // Arrange
            HalLink link = new HalLink("http://www.google.com", true, "sometype", "depreciation", "somename",
                                       "someprofile", "someTitle", "en");
            HalLinkConverter converter = new HalLinkConverter();
            StringWriter sw;
            JsonTextWriter writer;
            var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
            string expectedOutput =
                "{\r\n  \"name\": \"somename\",\r\n  \"href\": \"http://www.google.com\",\r\n  \"templated\": true,\r\n  \"type\": \"sometype\",\r\n  \"depreciation\": \"depreciation\",\r\n  \"profile\": \"someprofile\",\r\n  \"title\": \"someTitle\",\r\n  \"hrefLang\": \"en\"\r\n}";

            // Act
            converter.WriteJson(writer, link, serializer);

            // Assert
            Assert.Equal(expectedOutput, sw.ToString());
        }

        [Fact]
        public void WriteJson_WithOnlyHrefProperty_GeneratesCorrectJsonObject()
        {
            // Arrange
            HalLink link = new HalLink("http://www.google.com");
            HalLinkConverter converter = new HalLinkConverter();
            StringWriter sw;
            JsonTextWriter writer;
            var serializer = TestHelper.CreateWriterSerializer(out sw, out writer);
            string expectedOutput = "{\r\n  \"href\": \"http://www.google.com\"\r\n}";

            // Act
            converter.WriteJson(writer, link, serializer);

            // Assert
            Assert.Equal(expectedOutput, sw.ToString());
        }

        [Fact]
        public void CanConvert_HalLinkType_ReturnsTrue()
        {
            // Arrange
            HalLinkConverter converter = new HalLinkConverter();

            // Act
            var result = converter.CanConvert(typeof(HalLink));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ReadJson_WithStringReaderInputReadTwice_ReturnsStringSuccessfully()
        {
            // Arrange
            HalLinkConverter converter = new HalLinkConverter();
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
            HalLinkConverter converter = new HalLinkConverter();
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
