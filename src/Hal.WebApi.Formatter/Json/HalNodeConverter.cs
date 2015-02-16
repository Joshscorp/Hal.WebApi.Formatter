using Newtonsoft.Json;
using System;

namespace Hal.WebApi.Formatter.Json
{
    public class HalNodeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(HalNode).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var node = value as HalNode;

            if (node != null)
            {
                writer.WritePropertyName(node.Key);
                if (node.Value is HalResource || node.Value is HalLink || node.Value is HalNode)
                {
                    if (node.Value is HalNode)
                        writer.WriteStartObject();

                    serializer.Serialize(writer, node.Value);

                    if (node.Value is HalNode)
                        writer.WriteEndObject();
                }
                else
                    writer.WriteValue(node.Value);
            }
        }
    }
}
