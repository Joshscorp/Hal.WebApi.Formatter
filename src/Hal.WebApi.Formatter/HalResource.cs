using System.Collections.Generic;

namespace Hal.WebApi.Formatter
{
    /// <summary>
    /// A Resource Object represents a resource.
    /// </summary>
    public class HalResource
    {
        public const string LinksProperty = "_links";
        public const string EmbeddedProperty = "_embedded";

        private readonly HalReservedEmbeddedResources embedded;
        private readonly HalReservedLinks links;
        private readonly HalNodes content;

        /// <summary>
        /// A Resource Object represents a resource.
        /// </summary>
        /// <param name="selfLink"></param>
        /// <param name="curiesLink"></param>
        public HalResource(HalLink selfLink, IEnumerable<HalLinkCurie> curiesLink = null)
        {
            this.links = new HalReservedLinks(selfLink, curiesLink);
            this.embedded = new HalReservedEmbeddedResources();
            this.content = new HalNodes();
        }

        /// <summary>
        /// The reserved "_links" property is OPTIONAL.
        /// </summary>
        public HalReservedLinks Links { get { return this.links; } }
        public HalReservedEmbeddedResources Embedded { get { return this.embedded; } }
        public HalNodes Content { get { return this.content; } }
    }
}
