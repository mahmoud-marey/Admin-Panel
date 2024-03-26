namespace Sherka.Web.Core.ViewModels
{
	public class HomeViewModel
	{
		public string? SiteLogo { get; set; } = null!;
		public string SiteTitle { get; set; } = null!;
		public string SiteDescription { get; set; } = null!;
		public string HeaderTitle { get; set; } = null!;
		public string? Keywords { get; set; }
		public string HeaderDescription { get; set; } = null!;
		public string ImageUrl { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber1 { get; set; }
		public string? PhoneNumber2 { get; set; }
		public string? WhatsappNumber { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? Address { get; set; } 
        public string? GoogleMapAddress { get; set; }
        public string? About { get; set; }
		public IEnumerable<Post>? Posts { get; set; }

	}
	
}
