

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Data;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]

    public class PostsController : Controller
	{
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        private List<string> allowedImageExtentisions = new() { ".jpg", ".png", ".jpeg"};
        private int maxAllowedSize = 3145728;

        public PostsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = environment;
        }

        public IActionResult Index()
		{
            var posts = context.Posts.AsNoTracking().ToList();
            var viewModel = mapper.Map<IEnumerable<PostViewModel>>(posts);
            var logoUrl = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = logoUrl;
			return View(viewModel);
		}
		[HttpGet]
        public IActionResult Create()
        {
            var viewModel = new PostFormViewModel();
            if (context.Photos.Any())
            {
                var photos = context.Photos.ToList();
                IList<string> imgsString = new List<string>();
                foreach(var photo in photos)
                {
                    var imgString = $"{photo.ImgName},{photo.ImgUrl}";
                    imgsString.Add(imgString);
                }
                viewModel.DiscriptionImagesString = imgsString;
            }
            var logoUrl = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = logoUrl;
            return View("Form", viewModel);
        }
		[HttpPost]
        public IActionResult Create(PostFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form",model);

            var post = mapper.Map<Post>(model);
            
            if (model.Image is not null)
            {
                var extension = Path.GetExtension(model.Image.FileName);
                if (!allowedImageExtentisions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image),Errors.NotAllowedExtension);
                    return View("Form",model);
                }
                if(model.Image.Length > maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                    return View("Form", model);
                }

                var imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/Posts", imageName);

                using var stream = System.IO.File.Create(path);
                model.Image.CopyTo(stream);

                post.ImageUrl = imageName;
            }

            context.Add(post);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = context.Posts.Find(id);
            if (post is null)
                return NotFound();

            var viewModel = mapper.Map<PostFormViewModel>(post);
            var logoUrl = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = logoUrl;
            return View("Form", viewModel);
        }
        [HttpPost]
        public IActionResult Edit(PostFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var post = context.Posts.Find(model.Id);
            if (post is null)
                return NotFound();

            if (model.Image is not null)
            {
                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    var oldImagePath = Path.Combine($"{webHostEnvironment.WebRootPath}/images/Posts", post.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                var extension = Path.GetExtension(model.Image.FileName);
                if (!allowedImageExtentisions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                    return View("Form", model);
                }
                if (model.Image.Length > maxAllowedSize)
                {
                    ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                    return View("Form", model);
                }

                var imageName = $"{Guid.NewGuid()}{extension}";
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/images/Posts", imageName);

                using var stream = System.IO.File.Create(path);
                model.Image.CopyTo(stream);

                model.ImageUrl = imageName;
            }
            else
            {
                model.ImageUrl = post.ImageUrl;
            }
            post = mapper.Map(model, post);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var post = context.Posts.Find(id);
            if (post is null)
                return NotFound();

            context.Posts.Remove(post);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public IActionResult ViewPost(int id)
        {
            var post = context.Posts.Find(id);
            if (post is null)
                return NotFound();
            var viewModel = mapper.Map<ViewPostViewModel>(post);
            return View(viewModel);
        }
    }
}
