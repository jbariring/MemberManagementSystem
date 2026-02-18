using MemberManagement.Domain.Entities;
using MemberManagement.Infrastructure;
using MemberManagement.Web.ViewModels;
using MemberManagement.Web.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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

        // ------------------- Helper Method -------------------
        private List<SelectListItem> GetActiveBranches(int? selectedBranchId = null)
        {
            return _context.Branches
                .Where(b => b.IsActive)
                .OrderBy(b => b.Name)
                .Select(b => new SelectListItem
                {
                    Value = b.BranchID.ToString(),
                    Text = b.Name,
                    Selected = selectedBranchId.HasValue && b.BranchID == selectedBranchId.Value
                })
                .ToList();
        }

        // Member list page with search & filter
        public IActionResult MemberListPage(string? searchLastName, string? branch, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Members
                .Where(m => m.IsActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchLastName))
            {
                var searchLower = searchLastName.Trim().ToLower();
                query = query.Where(m => m.LastName != null && m.LastName.ToLower().Contains(searchLower));
            }

            if (!string.IsNullOrEmpty(branch))
            {
                var branchLower = branch.Trim().ToLower();
                query = query.Where(m => m.Branch != null && m.Branch.Name.ToLower() == branchLower && m.Branch.IsActive);
            }

            int totalMembers = query.Count();
            int startItem = totalMembers == 0 ? 0 : ((pageNumber - 1) * pageSize) + 1;
            int endItem = totalMembers == 0 ? 0 : Math.Min(pageNumber * pageSize, totalMembers);

            var members = query
                .Include(m => m.Branch)
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
                    Branch = m.Branch != null ? m.Branch.Name : string.Empty,
                    ContactNo = m.ContactNo,
                    Email = m.Email,
                    IsActive = m.IsActive,
                    DateCreated = m.DateCreated
                })
                .ToList();

            var branches = _context.Branches
                .Where(b => b.IsActive)
                .OrderBy(b => b.Name)
                .Select(b => b.Name)
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

        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreateMemberVM
            {
                Branches = GetActiveBranches()
            };
            return View(vm);
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(CreateMemberVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Branches = GetActiveBranches(model.BranchID);
                return View(model);
            }

            var branchEntity = _context.Branches.Find(model.BranchID);
            if (branchEntity == null || !branchEntity.IsActive)
            {
                ModelState.AddModelError(nameof(model.BranchID), "Please select a valid active branch.");
                model.Branches = GetActiveBranches(model.BranchID);
                return View(model);
            }

            var member = new Member(
                model.FirstName,
                model.LastName,
                model.BirthDate,
                model.Address,
                model.BranchID,
                model.ContactNo,
                model.Email
            );

            try
            {
                _context.Members.Add(member);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Database error: {innerMessage}");
                model.Branches = GetActiveBranches(model.BranchID);
                return View(model);
            }

            return RedirectToAction(nameof(MemberListPage));
        }

        // ------------------- Edit -------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _context.Members
                   .Include(m => m.Branch)
                   .FirstOrDefault(m => m.MemberID == id);

            if (member == null) return NotFound();

            var model = new EditMemberVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Address = member.Address,
                BranchID = member.Branch?.BranchID ?? 0,
                ContactNo = member.ContactNo,
                Email = member.Email,
                Branches = GetActiveBranches(member.Branch?.BranchID)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditMemberVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Branches = GetActiveBranches(model.BranchID);
                return View(model);
            }

            var member = _context.Members.Find(model.MemberID);
            if (member == null) return NotFound();

            var branchEntity = _context.Branches.Find(model.BranchID);
            if (branchEntity == null || !branchEntity.IsActive)
            {
                ModelState.AddModelError(nameof(model.BranchID), "Please select a valid active branch.");
                model.Branches = GetActiveBranches(model.BranchID);
                return View(model);
            }

            member.UpdateDetails(
                model.FirstName,
                model.LastName,
                model.BirthDate,
                model.Address,
                branchEntity.BranchID,
                model.ContactNo,
                model.Email
            );

            _context.SaveChanges();
            return RedirectToAction(nameof(MemberListPage));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var member = _context.Members
                    .Include(m => m.Branch) // <-- load branch here
                    .FirstOrDefault(m => m.MemberID == id);

            var model = new MemberDetailsVM
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Address = member.Address,
                Branch = member.Branch?.Name,
                ContactNo = member.ContactNo,
                Email = member.Email,
                IsActive = member.IsActive,
                DateCreated = member.DateCreated
            };

            return View(model);
        }


        // GET: Delete
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

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int memberId)
        {
            var member = _context.Members.Find(memberId);
            if (member == null) return NotFound();

            member.Deactivate();
            _context.SaveChanges();

            return RedirectToAction(nameof(MemberListPage));
        }
    }
}
