

using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]

    public class UsersController : Controller
	{
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;


        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext context)
        {

            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
        }


        public async Task<IActionResult> Index()
		{
            var users = await userManager.Users.ToListAsync();
            var viewModel = mapper.Map<IEnumerable<UserViewModel>>(users);
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
		}
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new UserFormViewModel();
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
        }
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            ApplicationUser admin = new()
            {
                UserName = model.UserName ,
                FullName = model.FullName ,
                Email = model.Email,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, model.Password);
            await userManager.AddToRoleAsync(admin, AppRoles.Admin);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user is null)
                return NotFound();
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
