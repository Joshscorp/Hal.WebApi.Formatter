using Newtonsoft.Json;
using System;

namespace Hal.WebApi.Formatter.Json
{
    public class HalLinkConverter : JsonConverter
    {
        #region CanConvert override
        public override bool CanConvert(Type objectType)
        {
            return typeof(HalLink).IsAssignableFrom(objectType);
        }
        #endregion

        #region ReadJson override
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }
        #endregion

        #region WriteJson override
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var link = value as HalLink;

            if (link != null)
            {
                writer.WriteStartObject();

                if (link.Name != null)
                {
                    writer.WritePropertyName(HalLink.NameKey);
                    writer.WriteValue(link.Name);
                }

                writer.WritePropertyName(HalLink.HrefKey);
                writer.WriteValue(link.Href);

                if (link.Templated)
                {
                    writer.WritePropertyName(HalLink.TemplatedKey);
                    writer.WriteValue(link.Templated);
                }

                if (link.Type != null)
                {
                    writer.WritePropertyName(HalLink.TypeKey);
                    writer.WriteValue(link.Type);
                }

                if (link.Depreciation != null)
                {
                    writer.WritePropertyName(HalLink.DepreciationKey);
                    writer.WriteValue(link.Depreciation);
                }

                if (link.Profile != null)
                {
                    writer.WritePropertyName(HalLink.ProfileKey);
                    writer.WriteValue(link.Profile);
                }

                if (link.Title != null)
                {
                    writer.WritePropertyName(HalLink.TitleKey);
                    writer.WriteValue(link.Title);
                }

                if (link.HrefLang != null)
                {
                    writer.WritePropertyName(HalLink.HrefLangKey);
                    writer.WriteValue(link.HrefLang);
                }

                writer.WriteEndObject();
            }
        }
        #endregion        
    }
}
