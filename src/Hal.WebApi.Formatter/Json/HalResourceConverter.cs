using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hal.WebApi.Formatter.Json
{
    public class HalResourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(HalResource).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var resource = value as HalResource;

            if (resource != null)
            {
                writer.WriteStartObject();

                // _links property
                if (resource.Links.Any())
                    WriteLinksProperty(writer, serializer, resource);

                // _embedded property
                if (resource.Embedded.Any())
                    WriteEmbeddedProperty(writer, serializer, resource);

                // Content         
                if (resource.Content.Any())
                    WriteContent(writer, serializer, resource);

                writer.WriteEndObject();
            }
        }

        #region Private static methods
        private static void WriteContent(JsonWriter writer, JsonSerializer serializer, HalResource resource)
        {
            foreach (HalNode contentItem in resource.Content)
            {
                serializer.Serialize(writer, contentItem);
            }
        }

        private static void WriteLinksProperty(JsonWriter writer, JsonSerializer serializer, HalResource resource)
        {
            writer.WritePropertyName(HalResource.LinksProperty);
            writer.WriteStartObject();
            // self property
            WriteSelfLink(writer, serializer, resource);
            // curries property                
            var curiesLinks = resource.Links.GetCuriesNode().Value as List<HalLinkCurie>;
            if (curiesLinks.Any())
                WriteCuriesLink(writer, serializer, curiesLinks);

            // write other links
            WriteOtherLinks(writer, serializer, resource);

            writer.WriteEndObject();
        }

        private static void WriteSelfLink(JsonWriter writer, JsonSerializer serializer, HalResource resource)
        {
            writer.WritePropertyName(HalReservedLinks.SelfProperty);
            serializer.Serialize(writer, resource.Links.GetSelfNode().Value);
        }

        private static void WriteCuriesLink(JsonWriter writer, JsonSerializer serializer, IEnumerable<HalLinkCurie> curiesLinks)
        {
            writer.WritePropertyName(HalReservedLinks.CuriesProperty);
            writer.WriteStartArray();
            foreach (var curieLink in curiesLinks)
            {
                serializer.Serialize(writer, curieLink);
            }
            writer.WriteEndArray();
        }

        private static void WriteOtherLinks(JsonWriter writer, JsonSerializer serializer, HalResource resource)
        {
            foreach (var link in resource.Links.Where(i => i.Key != HalReservedLinks.SelfProperty &&
                i.Key != HalReservedLinks.CuriesProperty))
            {
                writer.WritePropertyName(link.Key);
                serializer.Serialize(writer, link.Value);
            }
        }

        private static void WriteEmbeddedProperty(JsonWriter writer, JsonSerializer serializer, HalResource resource)
        {
            writer.WritePropertyName(HalResource.EmbeddedProperty);
            writer.WriteStartObject();

            if (resource.Embedded.Any())
            {
                foreach (HalNode<IEnumerable<HalResource>> embeddedItem in resource.Embedded)
                {
                    writer.WritePropertyName(embeddedItem.Key);

                    var embeddedItemValue = embeddedItem.Value.ToList();
                    var count = embeddedItemValue.Count;

                    // If there are more than 1 item
                    if (count > 1)
                    {
                        writer.WriteStartArray();
                        foreach (var contentResource in embeddedItemValue)
                        {
                            serializer.Serialize(writer, contentResource);
                        }
                        writer.WriteEndArray();
                    }
                    else if (count == 1) // If there is only one item
                    {
                        var contentResource = embeddedItemValue.First();
                        serializer.Serialize(writer, contentResource);
                    }
                }
            }

            writer.WriteEndObject();
        }
        #endregion
    }
}
