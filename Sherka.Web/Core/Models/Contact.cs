namespace Sherka.Web.Core.Models
{
	public class Contact
	{
		public int Id { get; set; }
		public string Email { get; set; } = null!;
		public string PhoneNumber1 { get; set; } = null!;
		public string? PhoneNumber2 { get; set; }
		public string? WhatsappNumber { get; set; }
		public string? FacebookLink { get; set; }
		public string? InstagramLink { get; set; }
		public string? TwitterLink { get; set; }
        public string Address { get; set; } = null!;
        public string? GoogleMapAddress { get; set; } 
        public string? GoogleMapAddressSrc { get; set; } 

    }
}
