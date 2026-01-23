using MemberManagement.Domain.Entities;
using MemberManagement.Infrastructure;
using MemberManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MemberManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly MMSDbContext _context;

        public MemberController(MMSDbContext context)
        {
            _context = context;
        }

        public IActionResult MemberListPage()
        {
            var members = _context.Members
                .Where(m => m.IsActive == true)
                .Select(m => new MemberVM
                {
                    MemberID = m.MemberID,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    BirthDate = m.BirthDate,
                    Address = m.Address,
                    Branch = m.Branch,
                    ContactNo = m.ContactNo,
                    Email = m.Email,
                    IsActive = m.IsActive,
                    DateCreated = m.DateCreated
                })
                .ToList();

            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MemberVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var member = new Member
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Address = model.Address,
                Branch = model.Branch,
                ContactNo = model.ContactNo,
                Email = model.Email,
                IsActive = model.IsActive,
                DateCreated = DateTime.Now
            };

            _context.Members.Add(member);
            _context.SaveChanges();
            return RedirectToAction("MemberListPage");
        }

        [HttpGet] //Loads Record
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
                return NotFound();

            var model = new MemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Address = member.Address,
                Branch = member.Branch,
                ContactNo = member.ContactNo,
                Email = member.Email,
                IsActive = member.IsActive,
                DateCreated = member.DateCreated
            };

            return View(model);
        }

        [HttpPost] //Saves Changes
        public IActionResult Edit(MemberVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var member = _context.Members.Find(model.MemberID);
            if (member == null)
                return NotFound();

            member.FirstName = model.FirstName;
            member.LastName = model.LastName;
            member.BirthDate = model.BirthDate;
            member.Address = model.Address;
            member.Branch = model.Branch;
            member.ContactNo = model.ContactNo;
            member.Email = model.Email;
            member.IsActive = model.IsActive;

            _context.SaveChanges();

            return RedirectToAction("MemberListPage");
        }

        [HttpGet] //Detailed View
        public IActionResult Details(int id)
        {
            var member = _context.Members.Find(id);

            if (member == null)
                return NotFound();

            var model = new MemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Address = member.Address,
                Branch = member.Branch,
                ContactNo = member.ContactNo,
                Email = member.Email,
                IsActive = member.IsActive,
                DateCreated = member.DateCreated
            };

            return View(model);
        }
        [HttpGet] //Delete Confirmation View
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            var model = new MemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email
            };

            return View(model);
        }

        [HttpPost] //Delete Action
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int memberId)
        {
            var member = _context.Members.Find(memberId);
            if (member == null) return NotFound();

            member.IsActive = false;
            _context.SaveChanges();

            // DEBUG
            Console.WriteLine("Deactivated member " + memberId);

            return RedirectToAction(nameof(MemberListPage));
        }
    }
}
