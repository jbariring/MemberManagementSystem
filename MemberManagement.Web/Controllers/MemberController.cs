using MemberManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MemberManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult MemberListPage()
        {
            var members = new List<MemberVM>(); // load from DB later
            return View(members);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(MemberVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Save to database

            return RedirectToAction("MemberListPage");
        }
    }
}

