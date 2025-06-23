using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Anarcareweb.Models;
using AnarcareWeb.Models;
using AnarcareWeb.Data;

namespace Anarcareweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View("aboutus");
        }

        public IActionResult Services()
        {
            return View("services");
        }

        public IActionResult HospiceCare()
        {
            return View("hospicecare");
        }

        public IActionResult HomeHealth()
        {
            return View("homehealth");
        }

        public IActionResult Referrals()
        {
            return View("referrals");
        }

        public IActionResult Careers()
        {
            return View("careers");
        }

        public IActionResult Employment()
        {
            return View("employment");
        }

        public IActionResult Volunteer()
        {
            return View("volunteer");
        }

        [HttpPost]
        public IActionResult SubmitContact(ContactMessage contact)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(contact);
                _context.SaveChanges();
                TempData["Success"] = "Thank you for contacting us!";
                return RedirectToAction("Index");
            }

            return View("Index", contact);
        }

        [HttpPost]
        public IActionResult SubmitReferral(Referral referral)
        {
            if (ModelState.IsValid)
            {
                _context.Referrals.Add(referral);
                _context.SaveChanges();
                TempData["Success"] = "Referral submitted successfully!";
                return RedirectToAction("Referrals");
            }

            return View("Referrals", referral);
        }

        [HttpPost]
        public async Task<IActionResult> Employment(IFormCollection form, IFormFile resume)
        {
            var employment = new Employment
            {
                Name = form["name"],
                Email = form["email"],
                Phone = form["phone"],
                Position = form["position"],
                Comments = form["comments"]
            };

            if (resume != null && resume.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, resume.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resume.CopyToAsync(stream);
                }

                employment.ResumeFileName = resume.FileName;
            }

            _context.Employments.Add(employment);
            _context.SaveChanges();

            TempData["Success"] = "Thank you for applying!";
            return RedirectToAction("Employment");
        }

        [HttpPost]
        public IActionResult Volunteer(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(volunteer);
                _context.SaveChanges();
                TempData["Success"] = "Thank you for volunteering!";
                return RedirectToAction("Volunteer");
            }

            return View(volunteer);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
