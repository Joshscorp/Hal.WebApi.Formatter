using System;
using System.Text.RegularExpressions;

namespace Hal.WebApi.Formatter
{
    /// <summary>
    /// A Link Object represents a hyperlink from the containing resource to
    /// a URI.
    /// </summary>
    public class HalLink
    {
        public const string HrefKey = "href";
        public const string TemplatedKey = "templated";
        public const string TypeKey = "type";
        public const string DepreciationKey = "depreciation";
        public const string NameKey = "name";
        public const string ProfileKey = "profile";
        public const string TitleKey = "title";
        public const string HrefLangKey = "hrefLang";

        private readonly string href;
        private readonly bool templated;
        private readonly string type;
        private readonly string depreciation;
        private readonly string name;
        private readonly string profile;
        private readonly string title;
        private readonly string hrefLang;

        /// <summary>
        /// A Link Object represents a hyperlink from the containing resource to a URI.
        /// </summary>
        /// <param name="href">Its value is either a URI [RFC3986] or a URI Template [RFC6570].</param>
        /// <param name="templated">Its value is boolean and SHOULD be true when the Link Object's "href" property is a URI Template.</param>
        /// <param name="type">Its value is a string used as a hint to indicate the media type expected when dereferencing the target resource.</param>
        /// <param name="depreciation"> Its presence indicates that the link is to be deprecated (i.e. removed) at a future date.  Its value is a URL that SHOULD provide further information about the deprecation.</param>
        /// <param name="name">Its value MAY be used as a secondary key for selecting Link Objects which share the same relation type.</param>
        /// <param name="profile">Its value is a string which is a URI that hints about the profile (as defined by [I-D.wilde-profile-link]) of the target resource.</param>
        /// <param name="title">Its value is a string and is intended for labelling the link with ahuman-readable identifier (as defined by [RFC5988]).</param>
        /// <param name="hrefLang">Its value is a string and is intended for indicating the language of the target resource (as defined by [RFC5988]).</param>
        public HalLink(string href, bool templated = false, string type = null, string depreciation = null,
                    string name = null, string profile = null, string title = null, string hrefLang = null)
        {
            if (href == null)
                throw new ArgumentNullException("href");
            if (string.IsNullOrWhiteSpace(href))
                throw new ArgumentException("Input cannot be empty", "href");

            if (Regex.Match(href, "{.*?}").Success)
                templated = true;

            this.href = href;
            this.templated = templated;
            this.type = type;
            this.depreciation = depreciation;
            this.name = name;
            this.profile = profile;
            this.title = title;
            this.hrefLang = hrefLang;
        }

        /// <summary>
        /// The "href" property is REQUIRED.
        /// Its value is either a URI [RFC3986] or a URI Template [RFC6570].
        /// If the value is a URI Template then the Link Object SHOULD have a
        /// "templated" attribute whose value is true.
        /// </summary>
        public string Href
        {
            get { return this.href; }
        }

        /// <summary>
        /// The "templated" property is OPTIONAL.
        /// Its value is boolean and SHOULD be true when the Link Object's "href"
        /// property is a URI Template.
        /// Its value SHOULD be considered false if it is undefined or any other
        /// value than true.
        /// </summary>
        public bool Templated
        {
            get { return this.templated; }
        }

        /// <summary>
        /// The "type" property is OPTIONAL.
        /// Its value is a string used as a hint to indicate the media type
        /// expected when dereferencing the target resource.
        /// </summary>
        public string Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// The "deprecation" property is OPTIONAL.
        /// Its presence indicates that the link is to be deprecated (i.e.
        /// removed) at a future date.  Its value is a URL that SHOULD provide
        /// further information about the deprecation.
        /// A client SHOULD provide some notification (for example, by logging a
        /// warning message) whenever it traverses over a link that has this
        /// property.  The notification SHOULD include the deprecation property's
        /// value so that a client manitainer can easily find information about
        /// the deprecation.
        /// </summary>
        public string Depreciation
        {
            get { return this.depreciation; }
        }

        /// <summary>
        /// The "name" property is OPTIONAL.
        /// Its value MAY be used as a secondary key for selecting Link Objects
        /// which share the same relation type.        
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        // The "profile" property is OPTIONAL.
        // Its value is a string which is a URI that hints about the profile (as
        // defined by [I-D.wilde-profile-link]) of the target resource.                
        /// </summary>
        public string Profile
        {
            get { return this.profile; }
        }

        /// <summary>
        /// The "title" property is OPTIONAL.
        /// Its value is a string and is intended for labelling the link with a
        /// human-readable identifier (as defined by [RFC5988]).        
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }

        /// <summary>
        /// The "hreflang" property is OPTIONAL.
        /// Its value is a string and is intended for indicating the language of
        /// the target resource (as defined by [RFC5988]).        
        /// </summary>
        public string HrefLang
        {
            get { return this.hrefLang; }
        }
    }

    public class HalLinkCurie : HalLink
    {
        /// <summary>
        /// A Link Object represents a hyperlink from the containing resource to a URI.
        /// </summary>
        /// <param name="href">Its value is either a URI [RFC3986] or a URI Template [RFC6570].</param>
        /// /// <param name="name">Its value MAY be used as a secondary key for selecting Link Objects which share the same relation type.</param>
        /// <param name="templated">Its value is boolean and SHOULD be true when the Link Object's "href" property is a URI Template.</param>
        /// <param name="type">Its value is a string used as a hint to indicate the media type expected when dereferencing the target resource.</param>
        /// <param name="depreciation"> Its presence indicates that the link is to be deprecated (i.e. removed) at a future date.  Its value is a URL that SHOULD provide further information about the deprecation.</param>        
        /// <param name="profile">Its value is a string which is a URI that hints about the profile (as defined by [I-D.wilde-profile-link]) of the target resource.</param>
        /// <param name="title">Its value is a string and is intended for labelling the link with ahuman-readable identifier (as defined by [RFC5988]).</param>
        /// <param name="hrefLang">Its value is a string and is intended for indicating the language of the target resource (as defined by [RFC5988]).</param>
        public HalLinkCurie(string href, string name, bool templated = false, string type = null, string depreciation = null,
                    string profile = null, string title = null, string hrefLang = null) :
            base(href, templated, type, depreciation, name, profile, title, hrefLang)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Parameter cannot be empty.", "name");
        }
    }
}
