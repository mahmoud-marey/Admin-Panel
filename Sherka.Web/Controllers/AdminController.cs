using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;

        private List<string> allowedImageExtentisions = new() { ".jpg", ".png", ".jpeg" };
        private List<string> allowedImageExtentisionslogo = new() { ".ico" };
        private long maxAllowedSize = 10485760;

        public AdminController(IMapper mapper, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.mapper = mapper;
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            if (!context.AdminHome.Any())
            {
                return View(seedAdmin());
            }
            var adminHome = context.AdminHome.FirstOrDefault();
            if (adminHome is null)
            {
                return View(seedAdmin());
            }

            AdminHomeViewModel viewModel = new()
            {
                HeaderBackgroundUrl = adminHome.HeaderBackgroundUrl,
                SiteLogoUrl = adminHome.SiteLogoUrl,
                SiteTitle = adminHome.SiteTitle,
                Keywords = adminHome.Keywords,
                SiteDiscription = adminHome.SiteDiscription,
                HeaderTitle = adminHome.HeaderTitle,
                HeaderDiscription = adminHome.HeaderDiscription,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(AdminHomeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var adminHome = context.AdminHome.FirstOrDefault();
            if (adminHome is null)
                return NotFound();

            if (model.HeaderBackground is not null)
            {
                var extension = Path.GetExtension(model.HeaderBackground.FileName);
                if (!allowedImageExtentisions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.HeaderBackground), Errors.NotAllowedExtension);
                    return View(model);
                }
                if (model.HeaderBackground.Length > maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.HeaderBackground), Errors.MaxSize);
                    return View(model);
                }

                if (!string.IsNullOrEmpty(adminHome.HeaderBackgroundUrl))
                {
                    var oldImagePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/backgrounds", adminHome.HeaderBackgroundUrl);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                var imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/backgrounds", imageName);

                using var stream = System.IO.File.Create(path);
                model.HeaderBackground.CopyTo(stream);
                model.HeaderBackgroundUrl = imageName;
            }
            else
            {
                model.HeaderBackgroundUrl = adminHome.HeaderBackgroundUrl;
            }

            if (model.SiteLogo is not null)
            {
                var extension = Path.GetExtension(model.SiteLogo.FileName);
                if (!allowedImageExtentisionslogo.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.HeaderBackground), Errors.NotAllowedExtension);
                    return View(model);
                }
                if (model.SiteLogo.Length > maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.HeaderBackground), Errors.MaxSize);
                    return View(model);
                }

                if (!string.IsNullOrEmpty(adminHome.SiteLogoUrl))
                {
                    var oldImagePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/sitelogos", adminHome.SiteLogoUrl);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }
                var imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/sitelogos", imageName);

                using var stream = System.IO.File.Create(path);
                model.SiteLogo.CopyTo(stream);
                model.SiteLogoUrl = imageName;
            }
            else
            {
                model.SiteLogoUrl = adminHome.HeaderBackgroundUrl;
            }

            adminHome = mapper.Map(model, adminHome);
            adminHome.About = "<p>About<p>";
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }





        private AdminHomeViewModel seedAdmin()
        {
            AdminHome adminHome = new()
            {
                SiteLogoUrl = "/favicon.ico",
                SiteTitle = "SiteTitle",
                SiteDiscription = "SiteDiscription",
                HeaderTitle = "HeaderTitle",
                HeaderDiscription = "HeaderDiscription",
                About = "<p>About<p>"
            };
            AdminHomeViewModel viewModel = new()
            {
                SiteLogoUrl = adminHome.SiteLogoUrl,
                SiteTitle = adminHome.SiteTitle,
                SiteDiscription = adminHome.SiteDiscription,
                HeaderTitle = adminHome.HeaderTitle,
                HeaderDiscription = adminHome.HeaderDiscription,
            };
            context.Add(adminHome);
            context.SaveChanges();
            return viewModel;
        }
    }
}
