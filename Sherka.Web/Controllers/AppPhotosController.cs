using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]

    public class AppPhotosController : Controller
	{
		private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;

        private List<string> allowedImageExtentisions = new() { ".jpg", ".png", ".jpeg" };
        private int maxAllowedSize = 3145728;

        public AppPhotosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
		{
			this.context = context;
			this.webHostEnvironment = webHostEnvironment;
			this.mapper = mapper;
		}

		public IActionResult Index()
        {
            
            var photos = context.Photos.AsNoTracking().ToList();
            var viewModel = mapper.Map<IEnumerable<PhotoViewModel>>(photos);
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
		}
		[HttpGet]
        public IActionResult Create()
        {
            var viewModel = new PhotoViewModel();
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
        }
		[HttpPost]
        public IActionResult Create(PhotoViewModel model)
        {
            if(!ModelState.IsValid)
				return View(model);
            var extension = Path.GetExtension(model.Image.FileName);
            if (!allowedImageExtentisions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                return View(model);
            }

            if (model.Image.Length > maxAllowedSize)
            {
                ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                return View(model);
            }
            var imageName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/appPhotos", imageName);

            using var stream = System.IO.File.Create(path);
            model.Image.CopyTo(stream);
            model.ImgUrl = $"../images/appPhotos/{imageName}";

            var photo = mapper.Map<Photo>(model);
            context.Add(photo);
            context.SaveChanges();

            return View();
        }
        public IActionResult Delete(int id)
        {
            var photo = context.Photos.Find(id);
            if (photo is null)
                return NotFound();

            context.Photos.Remove(photo);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
