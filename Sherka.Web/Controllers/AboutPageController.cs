using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]

    public class AboutPageController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AboutPageController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModel = new AboutViewModel();
            var about = context.About.FirstOrDefault();
            if (about is null)
                viewModel = seedAbout();
            else
                viewModel = mapper.Map<AboutViewModel>(about);
            if (context.Photos.Any())
            {
                var photos = context.Photos.ToList();
                IList<string> imgsString = new List<string>();
                foreach (var photo in photos)
                {
                    var imgString = $"{photo.ImgName},{photo.ImgUrl}";
                    imgsString.Add(imgString);
                }
                viewModel.DiscriptionImagesString = imgsString;
            }
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(AboutViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var about = context.About.FirstOrDefault();

            byte[] bytes = Encoding.Default.GetBytes(model.Body);
            model.Body = Encoding.UTF8.GetString(bytes);

            about = mapper.Map(model, about);
            context.SaveChanges();
            return View(model);
        }

        private AboutViewModel seedAbout()
        {
            var about = new About
            {
                Body = ""
            };
            context.Add(about);
            context.SaveChanges();
            about = context.About.FirstOrDefault();
            var viewModel = mapper.Map<AboutViewModel>(about);
            return viewModel;
        }
    }
}
