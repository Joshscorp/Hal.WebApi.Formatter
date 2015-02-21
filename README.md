# Hal.WebApi.Formatter
Hal Formatter for .Net WebApi

Created based on http://stateless.co/hal_specification.html

Latest NUGET Package available at https://www.nuget.org/packages/Hal.WebApi.Formatter/

Currently only supports the json flavour of HAL.  **application/hal+json**

**Add the formatter like so**
```
config.Formatters.Add(new JsonHalMediaTypeFormatter());
```

**Simply return the HalResource** 

If a request is made with application/hal+json, HAL format will be returned.

```
public HalResource Get(String id)
{
    HalResource resource = new HalResource(new HalLink("www.selflink.com"));
    // ... Random code here to create HalNodes etc...
    return resource;
}
```

**Example of embedded resources**

This example shows nesting of HalResources with simply a selflink only, but a resource can be created with more properties

```
HalReservedEmbeddedResources resources = new HalReservedEmbeddedResources();
string profileKey = "profile";
string detailKey = "details";
IEnumerable<HalResource> profileResources = new List<HalResource>()
    {
        new HalResource(new HalLink("www.profileItemA.com")),
        new HalResource(new HalLink("www.profileItemB.com")),
    };
string detailKey = "detail";
IEnumerable<HalResource> detailResources = new List<HalResource>()
    {
        new HalResource(new HalLink("www.detailItemA.com")),
        new HalResource(new HalLink("www.detailItemB.com")),
    };
resources.Add(profileKey, profileResources);
resources.Add(detailKey, detailResources);
```

**Creating CURIE Links**
```
var href = "www.selflink.com";
List<HalLinkCurie> curiesLink = new List<HalLinkCurie>()
    {
       new HalLinkCurie("www.google.com", "gn"),
       new HalLinkCurie("www.microsoft.com", "gn"),
       new HalLinkCurie("www.abc.com", "gn")
    };
HalLink selfLink = new HalLink(href);
var resource = new HalResource(selfLink, curiesLink);
```

**Creating properties**
```
HalResource resource = new HalResource(new HalLink("www.selflink.com"));

// new HalNode(propertyName, propertyValue);
var firstNameNode = new HalNode("FirstName", "John");
var lastNameNode = new HalNode("LastName", "Doe");

resource.Content.Add(firstNameNode);
resource.Content.Add(lastNameNode);
```

**HalLink class for reference**
```
public HalLink(string href, bool templated = false, string type = null, string depreciation = null, string name =                null, string profile = null, string title = null, string hrefLang = null);
```
**Adding Links to a resource**
```
HalResource resource = new HalResource(new HalLink("www.selflink.com"));

var link1 = new HalLink("www.link1.com", "Link number 1");
var link2 = new HalLink("www,link2.com", "Link number 2");

resource.Links.Add(link1);
resource.Links.Add(link2);
```
**Link collection**
```
var links = new List<HalLink>()
{
    new HalLink("www.petLink1.com", "Pet number 1"),
    new HalLink("www.petLink2.com", "Pet number 2"),
}
var petsLink = new HalNode("rel:pets", links);

resource.Links.Add(petsLink);
```
Check out the unit tests for now on how to use HalLink, HalNode/HalNodes, HalReservedEmbeddedResources..

*Coming soon...* 

*Dynamic Links and HalResource creation based on interfaces for more generic MULTI Hypermedia support and CLIENT side deserialisation support from JSON to HalResource object.*
