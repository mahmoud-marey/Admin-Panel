namespace Sherka.Web.Core.ViewModels
{
	public class UserFormViewModel
	{
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
