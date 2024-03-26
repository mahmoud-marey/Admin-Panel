namespace Sherka.Web.Core.ViewModels
{
	public class ContactViewModel
	{
        public string Email { get; set; } = null!;
        public string PhoneNumber1 { get; set; } = null!;
        public string? PhoneNumber2 { get; set; }
        public string Address { get; set; } = null!;
        [Display(Name = "Facebook Link")]
        public string? FacebookLink { get; set; }
        [Display(Name = "Instagram Link")]
        public string? InstagramLink { get; set; }
        [Display(Name = "Twitter Link")]
        public string? TwitterLink { get; set; }
        public string? GoogleMapAddress { get; set; }
        public string? GoogleMapAddressSrc { get; set; }

    }
}
