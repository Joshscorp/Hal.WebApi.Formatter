using System.Collections.Generic;

namespace Hal.WebApi.Formatter
{
    /// <summary>
    // The reserved "_embedded" property is OPTIONAL
    // It is an object whose property names are link relation types (as
    // defined by [RFC5988]) and values are either a Resource Object or an
    // array of Resource Objects.    
    /// </summary>
    public class HalReservedEmbeddedResources : HalNodes<HalNode<IEnumerable<HalResource>>>
    {
        /// <summary>
        /// Creates the reserved _embedded property
        /// Contains a list of property names, with either a Resource Object (One) or Array of Resource Objects (Many)
        /// </summary>
        public HalReservedEmbeddedResources()
        {

        }

        /// <summary>
        /// Adds to the list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void Add(string key, IEnumerable<HalResource> value)
        {
            this.Add(new HalNode<IEnumerable<HalResource>>(key, value));
        }
    }
}
