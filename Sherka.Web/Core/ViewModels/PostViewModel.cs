namespace Sherka.Web.Core.ViewModels
{
	public class PostViewModel
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string SEOTitle { get; set; } = null!;
        public string? SiteLogoUrl { get; set; } 
    }
}
