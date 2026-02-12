using MemberManagement.Domain.Entities;
using MemberManagement.Infrastructure;
using MemberManagement.Web.ViewModels;
using MemberManagement.Web.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace MemberManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly MMSDbContext _context;

        public MemberController(MMSDbContext context)
        {
            _context = context;
        }
        public IActionResult MemberListPage(string? searchLastName, string? branch, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Members
                .Where(m => m.IsActive)
                .AsQueryable();

            // Case-insensitive search by last name
            if (!string.IsNullOrEmpty(searchLastName))
            {
                var searchLower = searchLastName.Trim().ToLower();
                query = query.Where(m => m.LastName != null && m.LastName.ToLower().Contains(searchLower));
            }

            // Case-insensitive filter by branch
            if (!string.IsNullOrEmpty(branch))
            {
                var branchLower = branch.Trim().ToLower();
                query = query.Where(m => m.Branch != null && m.Branch.Trim().ToLower() == branchLower);
            }

            int totalMembers = query.Count();
            int startItem = totalMembers == 0 ? 0 : ((pageNumber - 1) * pageSize) + 1;
            int endItem = totalMembers == 0 ? 0 : Math.Min(pageNumber * pageSize, totalMembers);

            var members = query
                .OrderBy(m => m.MemberID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            // Distinct branches for dropdown
            var branches = _context.Members
                .Where(m => m.IsActive && !string.IsNullOrEmpty(m.Branch))
                .Select(m => m.Branch!)
                .Distinct()
                .OrderBy(b => b)
                .ToList();

            var vm = new MemberListVM
            {
                Members = members,
                SearchLastName = searchLastName,
                Branch = branch,
                Branches = new SelectList(branches, selectedValue: branch),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalMembers / pageSize),
                TotalMembers = totalMembers,
                StartItem = startItem,
                EndItem = endItem
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("MemberTable", vm);

            return View(vm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMemberVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var member = new Member(
                model.FirstName,
                model.LastName,
                model.BirthDate,
                model.Address,
                model.Branch,
                model.ContactNo,
                model.Email
            );

            _context.Members.Add(member);
            _context.SaveChanges();

            return RedirectToAction(nameof(MemberListPage));
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            var model = new EditMemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Address = member.Address,
                Branch = member.Branch,
                ContactNo = member.ContactNo,
                Email = member.Email
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(EditMemberVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var member = _context.Members.Find(model.MemberID);
            if (member == null) return NotFound();

            // Let the entity handle its own update
            member.UpdateDetails(
                model.FirstName,
                model.LastName,
                model.BirthDate,
                model.Address,
                model.Branch,
                model.ContactNo,
                model.Email
            );

            _context.SaveChanges();
            return RedirectToAction(nameof(MemberListPage));
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            var model = new MemberDetailsVM
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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null) return NotFound();

            var model = new DeleteMemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int memberId)
        {
            var member = _context.Members.Find(memberId);
            if (member == null) return NotFound();

            // ✅ Let the entity handle the deactivation
            member.Deactivate();

            _context.SaveChanges();

            return RedirectToAction(nameof(MemberListPage));
        }

    }
}
