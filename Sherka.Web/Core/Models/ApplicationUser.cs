using Microsoft.AspNetCore.Identity;

namespace Sherka.Web.Core.Models
{
	[Index(nameof(Email),IsUnique =true)]
	[Index(nameof(UserName),IsUnique =true)]
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; } = null!;
	}
}
