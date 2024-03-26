using Microsoft.AspNetCore.Mvc;
using Sherka.Web.Core.Models;
using Sherka.Web.Core.ViewModels;
using System.Diagnostics;

namespace Sherka.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var header = context.AdminHome.First();
            var about = context.About.FirstOrDefault();
            var post = context.Posts;
            var contact = context.Contacts.FirstOrDefault();
            if (header is null)
            {
                return BadRequest();
            }
            HomeViewModel viewModel = new()
            {
                SiteTitle = header.SiteTitle,
                SiteDescription = header.SiteDiscription,
                HeaderTitle = header.HeaderTitle,
                Keywords = header.Keywords,
                HeaderDescription = header.HeaderDiscription,
                ImageUrl = header.HeaderBackgroundUrl!
            };
            if(about is not null)
                viewModel.About = about.Body;

            if (post is not null)
                viewModel.Posts = post;
            if (contact is not null)
            {
                viewModel.Email = contact.Email;
                viewModel.PhoneNumber1 = contact.PhoneNumber1;
                viewModel.WhatsappNumber = contact.WhatsappNumber;
                viewModel.PhoneNumber2 = contact.PhoneNumber2;
                viewModel.FacebookLink = contact.FacebookLink;
                viewModel.InstagramLink = contact.InstagramLink;
                viewModel.TwitterLink = contact.TwitterLink;
                viewModel.Address = contact.Address;
                viewModel.GoogleMapAddress = contact.GoogleMapAddressSrc;
            }

            viewModel.SiteLogo = header.SiteLogoUrl;
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}