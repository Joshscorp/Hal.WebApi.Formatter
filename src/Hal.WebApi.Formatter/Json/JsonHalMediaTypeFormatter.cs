using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Hal.WebApi.Formatter.Json
{
    public class JsonHalMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private const string MediaTypeHeader = "application/hal+json";
        readonly HalResourceConverter resourceConverter = new HalResourceConverter();
        readonly HalNodeConverter nodeConverter = new HalNodeConverter();
        readonly HalLinkConverter linkConverter = new HalLinkConverter();

        public JsonHalMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeWithQualityHeaderValue(MediaTypeHeader));
            this.SerializerSettings.Converters.Add(resourceConverter);
            this.SerializerSettings.Converters.Add(nodeConverter);
            this.SerializerSettings.Converters.Add(linkConverter);
        }
    }
}
