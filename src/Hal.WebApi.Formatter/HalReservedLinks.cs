using System;
using System.Collections.Generic;

namespace Hal.WebApi.Formatter
{
    /// <summary>
    /// Contains links to other resources.
    /// It is an object whose property names are link relation types (as
    /// defined by [RFC5988]) and values are either a Link Object or an array
    /// of Link Objects.  The subject resource of these links is the Resource
    /// Object of which the containing "_links" object is a property.    
    /// </summary>
    public class HalReservedLinks : HalNodes
    {
        public const string SelfProperty = "self";
        public const string CuriesProperty = "curies";

        /// <summary>
        /// Reserved Links for a Resource
        /// It is an object whose property names are link relation types (as
        /// defined by [RFC5988]) and values are either a Link Object or an array
        /// of Link Objects.  The subject resource of these links is the Resource
        /// Object of which the containing "_links" object is a property.    
        /// </summary>
        /// <param name="selfLink">Each Resource Object SHOULD contain a 'self' link that corresponds with the IANA registered 'self' relation (as defined by [RFC5988]) whose target is the resource's URI.</param>
        /// <param name="curiesLink">Optional curies (compact uries) definition</param>
        public HalReservedLinks(HalLink selfLink, IEnumerable<HalLinkCurie> curiesLink = null)
        {
            if (selfLink == null)
                throw new ArgumentNullException("selfLink");

            base.Add(new HalNode(SelfProperty, selfLink));

            if (curiesLink != null)
                base.Add(new HalNode(CuriesProperty, curiesLink));
            else
                base.Add(new HalNode(CuriesProperty, new List<HalLinkCurie>()));
        }

        /// <summary>
        /// Gets the self node
        /// </summary>
        /// <returns></returns>
        public HalNode GetSelfNode()
        {
            return this[SelfProperty];
        }

        /// <summary>
        /// Gets the curies node
        /// </summary>
        /// <returns></returns>
        public HalNode GetCuriesNode()
        {
            return this[CuriesProperty];
        }

        /// <summary>
        /// Adds to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void Add(string key, object value)
        {
            if (key != null)
            {
                string lowerKey = key.ToLower();
                if (lowerKey == SelfProperty)
                    throw new ArgumentException(
                        String.Format("Cannot add a reserved key : {0}", SelfProperty), "key");
                if (lowerKey == CuriesProperty)
                    throw new ArgumentException(
                        String.Format("Cannot add a reserved key : {0}", CuriesProperty), "key");
            }
            base.Add(new HalNode(key, value));
        }
    }
}
