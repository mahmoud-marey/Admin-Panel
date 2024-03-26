namespace Sherka.Web.Core.Models
{
	public class AdminHome
	{
        public int Id { get; set; }
        [MaxLength(500)]
        [Display(Name ="Site title")]
        public string SiteTitle { get; set; } = null!;
        [Display(Name = "Site Discription")]
        public string SiteDiscription { get; set; } = null!;
        [MaxLength(500)]
        [Display(Name = "Header title")]
        public string HeaderTitle { get; set; } = null!;
        public string? Keywords { get; set; } 
        [MaxLength(500)]
        [Display(Name = "Header Discription")]
        public string HeaderDiscription { get; set; } = null!;
        public string About { get; set; } = null!;
        public string? SiteLogoUrl { get; set; }
        public string? HeaderBackgroundUrl { get; set; }
    }
}
