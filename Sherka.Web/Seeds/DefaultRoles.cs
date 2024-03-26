﻿using Microsoft.AspNetCore.Identity;

namespace Sherka.Web.Seeds
{
	public static class DefaultRoles
	{
		public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{	
				await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
			}
		}
	}
}
