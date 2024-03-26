using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Bookify.Web.Helpers;

[HtmlTargetElement("a", Attributes = "active-when")]
public class ActiveTag : TagHelper
{
	public string? ActiveWhen { get; set; }
	[ViewContext]
	[HtmlAttributeNotBound]
	public ViewContext? ViewContextData { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		if (ActiveWhen is null)
			return;

		var currentController = ViewContextData?.RouteData.Values["controller"]?.ToString();

		if (currentController!.Equals(ActiveWhen))
		{
			var classes = new StringBuilder(output.Attributes["class"]?.Value.ToString());
			classes.Append(" active");
			output.Attributes.SetAttribute("class", classes.ToString());
		}
	}
}
