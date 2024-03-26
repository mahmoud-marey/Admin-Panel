namespace Sherka.Web.Core.ViewModels
{
	public class AboutViewModel
	{
        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public IEnumerable<string>? DiscriptionImagesString { get; set; }

    }
}
