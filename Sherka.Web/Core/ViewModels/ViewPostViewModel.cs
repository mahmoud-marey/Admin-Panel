namespace Sherka.Web.Core.ViewModels
{
	public class ViewPostViewModel
	{
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string SEODiscription { get; set; } = null!;
        public string SEOKeyWords { get; set; } = null!;
        public string SEOTitle { get; set; } = null!;
    }
}
