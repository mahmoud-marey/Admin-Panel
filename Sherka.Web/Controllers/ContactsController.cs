using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Sherka.Web.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]

    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;


        public ContactsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            if(!context.Contacts.Any())
                return View(seedContact());

            var contact = context.Contacts.FirstOrDefault();
            var viewModel = mapper.Map<ContactViewModel>(contact);
            var siteLogo = context.AdminHome.Select(a => a.SiteLogoUrl).FirstOrDefault();
            ViewData["Logo"] = siteLogo;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var contact = context.Contacts.FirstOrDefault();
            if(contact is null)
                return View(model);
            if (!string.IsNullOrEmpty(model.GoogleMapAddress))
            {
                var strings = model.GoogleMapAddress.Split(" ");
                var src = strings[1];
                src = src.Substring(5, src.Length - 6);
                model.GoogleMapAddressSrc = src;

            }
            contact = mapper.Map(model, contact);
            context.SaveChanges();

            return View(model);
        }

        private ContactViewModel seedContact()
        {
            Contact contact = new()
            {
                Email = "email@company.com",
                PhoneNumber1 = "00000000",
                Address = "123-any street"
            };
            context.Add(contact);
            context.SaveChanges();
            return mapper.Map<ContactViewModel>(contact);
        }
    }
}
