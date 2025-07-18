// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using AnarcareWeb.Models;
// using AnarcareWeb.Data;
// using Azure.Storage.blobs;

// namespace AnarcareWeb.Controllers
// {
//     public class HomeController : Controller
//     {
//         private readonly ILogger<HomeController> _logger;
//         private readonly ApplicationDbContext _context;

//         public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
//         {
//             _logger = logger;
//             _context = context;
//         }

//         public IActionResult Index() => View();
//         public IActionResult Privacy() => View();
//         public IActionResult AboutUs() => View("aboutus");
//         public IActionResult Services() => View("services");
//         public IActionResult HospiceCare() => View("hospicecare");
//         public IActionResult HomeHealth() => View("homehealth");
//         public IActionResult Referrals() => View("referrals");
//         public IActionResult Careers() => View("careers");
//         public IActionResult Employment() => View("employment");
//         public IActionResult Volunteer() => View("volunteer");

//         // ----------- Contact Form -----------
//         [HttpPost]
//         public IActionResult SubmitContact(ContactMessage contact)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.ContactMessages.Add(contact);
//                 _context.SaveChanges();
//                 TempData["Success"] = "Thank you for contacting us!";
//                 return Redirect(Url.Action("Index", "Home") + "#contactForm");

//             }

//             return View("Index", contact);
//         }


//         // ----------- Referral Form -----------
//         [HttpPost]
//         public IActionResult SubmitReferral(IFormCollection form)
//         {
//             string selectedService = form["ServicesNeeded"].ToString() ?? "";
//             string otherService = form["OtherService"].ToString() ?? "";

//             string finalService = selectedService;
//             if (selectedService == "Other" && !string.IsNullOrWhiteSpace(otherService))
//             {
//                 finalService = $"Other: {otherService}";
//             }

//             var referral = new Referral
//             {
//                 PatientName = form["PatientName"].ToString() ?? "",
//                 Phone = form["Phone"].ToString() ?? "",
//                 ReferrerName = form["ReferrerName"].ToString() ?? "",
//                 Comments = form["Comments"].ToString() ?? "",
//                 ServicesNeeded = finalService,
//                 SubmittedAt = DateTime.Now
//             };



//             if (ModelState.IsValid)
//             {
//                 _context.Referrals.Add(referral);
//                 _context.SaveChanges();
//                 TempData["Success"] = "Referral submitted successfully!";
//                 return RedirectToAction("Referrals");
//             }

//             return View("~/Views/Home/referrals.cshtml", referral);
//         }



//         // ----------- Employment Form -----------
//         [HttpPost]
//         public async Task<IActionResult> SubmitEmployment(IFormCollection form, IFormFile resume)
//         {
//             var employment = new Employment
//             {
//                 FullName = form["FullName"].ToString() ?? "",
//                 Email = form["Email"].ToString() ?? "",
//                 Phone = form["Phone"].ToString() ?? "",
//                 Position = form["Position"].ToString() ?? "",
//                 Comments = form["Comments"].ToString() ?? "",
//                 SubmittedAt = DateTime.Now
//             };


//             if (resume != null && resume.Length > 0)
//             {
//                 var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
//                 if (!Directory.Exists(uploadsFolder))
//                     Directory.CreateDirectory(uploadsFolder);

//                 var filePath = Path.Combine(uploadsFolder, resume.FileName);
//                 using (var stream = new FileStream(filePath, FileMode.Create))
//                 {
//                     await resume.CopyToAsync(stream);
//                 }

//                 employment.ResumeFileName = resume.FileName;
//             }

//             _context.Employments.Add(employment);
//             _context.SaveChanges();

//             TempData["Success"] = "Thank you for applying!";
//             return RedirectToAction("Employment");
//         }

//         // ----------- Volunteer Form -----------
//         [HttpPost]
//         public IActionResult SubmitVolunteer(Volunteer volunteer)
//         {
//             if (ModelState.IsValid)
//             {
//                 volunteer.SubmittedAt = DateTime.Now;
//                 _context.Volunteers.Add(volunteer);
//                 _context.SaveChanges();
//                 TempData["Success"] = "Thank you for volunteering!";
//                 return RedirectToAction("Volunteer");
//             }

//             return View("volunteer", volunteer);
//         }
//             }
// }



using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnarcareWeb.Models;
using AnarcareWeb.Data;
// ---- New using statements ----
using Microsoft.Extensions.Configuration; // To access appsettings.json
using Azure.Storage.Blobs;              // The Azure Blob Storage SDK
using System;                           // For DateTime and Guid
using System.IO;                        // For Path
using System.Threading.Tasks;           // For async operations

namespace AnarcareWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index() => View();
        public IActionResult Privacy() => View();
        public IActionResult AboutUs() => View("aboutus");
        public IActionResult Services() => View("services");
        public IActionResult HospiceCare() => View("hospicecare");
        public IActionResult HomeHealth() => View("homehealth");
        public IActionResult Referrals() => View("referrals");
        public IActionResult Careers() => View("careers");
        public IActionResult Employment() => View("employment");
        public IActionResult Volunteer() => View("volunteer");

        // ----------- Contact Form -----------
        [HttpPost]
        public IActionResult SubmitContact(ContactMessage contact)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(contact);
                _context.SaveChanges();
                TempData["Success"] = "Thank you for contacting us!";
                // return Redirect(Url.Action("Index", "Home") + "#contactForm");
                return Redirect("/#contact");

            }
            return View("Index", contact);
        }

        // ----------- Referral Form -----------
        [HttpPost]
        public IActionResult SubmitReferral(IFormCollection form)
        {
            // This logic remains unchanged
            string selectedService = form["ServicesNeeded"].ToString() ?? "";
            string otherService = form["OtherService"].ToString() ?? "";
            string finalService = selectedService;
            if (selectedService == "Other" && !string.IsNullOrWhiteSpace(otherService))
            {
                finalService = $"Other: {otherService}";
            }
            var referral = new Referral
            {
                PatientName = form["PatientName"].ToString() ?? "",
                Phone = form["Phone"].ToString() ?? "",
                ReferrerName = form["ReferrerName"].ToString() ?? "",
                Comments = form["Comments"].ToString() ?? "",
                ServicesNeeded = finalService,
                SubmittedAt = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                _context.Referrals.Add(referral);
                _context.SaveChanges();
                TempData["Success"] = "Referral submitted successfully!";
                return RedirectToAction("Referrals");
            }
            return View("~/Views/Home/referrals.cshtml", referral);
        }

        // ----------- Employment Form (Corrected and Cleaned) -----------
        [HttpPost]
        public async Task<IActionResult> SubmitEmployment(IFormCollection form, IFormFile resume)
        {
            var employment = new Employment
            {
                FullName = form["FullName"].ToString() ?? "",
                Email = form["Email"].ToString() ?? "",
                Phone = form["Phone"].ToString() ?? "",
                Position = form["Position"].ToString() ?? "",
                Comments = form["Comments"].ToString() ?? "",
                SubmittedAt = DateTime.Now
            };

            if (resume != null && resume.Length > 0)
            {
                // Get base URL and SAS token from appsettings.json
                var baseUrl = _configuration.GetValue<string>("AzureBlobBaseUrl");
                var sasToken = _configuration.GetValue<string>("AzureBlobSasToken");

                // Construct the full container URI with the SAS token
                var containerUri = new Uri($"{baseUrl}?{sasToken}");

                // Create a unique name for the blob to prevent overwrites
                var blobName = $"{Path.GetFileNameWithoutExtension(resume.FileName)}-{Guid.NewGuid()}{Path.GetExtension(resume.FileName)}";

                // Create the container client using the SAS-enabled URI
                var containerClient = new BlobContainerClient(containerUri);

                // Upload the file stream from the web request
                await containerClient.UploadBlobAsync(blobName, resume.OpenReadStream());

                // Store the permanent, public-facing URL (without the token) in the database
                employment.ResumeUrl = $"{baseUrl}/{blobName}";
            }

            _context.Employments.Add(employment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thank you for applying!";
            return Redirect(Url.Action("Employment", "Home") + "#employmentForm");



        }

        // ----------- Volunteer Form -----------
        [HttpPost]
        public IActionResult SubmitVolunteer(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                volunteer.SubmittedAt = DateTime.Now;
                _context.Volunteers.Add(volunteer);
                _context.SaveChanges();
                TempData["Success"] = "Thank you for volunteering!";
            }
            return Redirect("/Home/Volunteer#volunteerForm");


        }

        public IActionResult SiteMap()
        {
            return View("sitemap");
        }

    }
}
