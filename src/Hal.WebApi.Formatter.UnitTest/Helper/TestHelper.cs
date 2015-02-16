using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Hal.WebApi.Formatter.UnitTest.Helper
{
    public class TestHelper
    {
        public static JsonSerializer CreateWriterSerializer(out StringWriter sw, out JsonTextWriter writer)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringBuilder sb = new StringBuilder();
            sw = new StringWriter(sb);
            writer = new JsonTextWriter(sw) { Formatting = Formatting.Indented };
            return serializer;
        }

        public static JsonSerializer CreateReaderSerializer(string json, out StringReader sr, out JsonTextReader reader)
        {
            JsonSerializer serializer = new JsonSerializer();
            sr = new StringReader(json);
            reader = new JsonTextReader(sr);
            return serializer;
        }
    }
}
