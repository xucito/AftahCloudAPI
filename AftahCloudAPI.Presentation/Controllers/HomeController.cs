using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using AftahCloudAPI.Presentation.Models;
using AftahCloud.Domain.Entities.ApplicationUsers;

namespace AftahCloudAPI.Presentation.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        SignInManager<ApplicationUser> _signInManager;

        public HomeController(IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var portalUrl = _configuration.GetSection("PublicUrls").GetValue<string>("Portal");

            ViewData["PortalUrl"] = portalUrl;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
