using MemberManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MemberManagement.Infrastructure;
using MemberManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemberManagement.Web.Controllers
{
    public class MembershipTypeController : Controller
    {
        private readonly MMSDbContext _context;

        public MembershipTypeController(MMSDbContext context)
        {
            _context = context;
        }

        // ------------------- LIST -------------------
        public IActionResult Index()
        {
            var list = _context.MembershipTypes
                .Select(m => new MembershipTypeViewModel
                {
                    MembershipTypeID = m.MembershipTypeID,
                    Name = m.Name,
                    IsActive = m.IsActive
                }).ToList();

            return View(list);
        }

        // ------------------- CREATE -------------------
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create and Edit", new MembershipTypeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MembershipTypeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Create and Edit", vm);

            try
            {
                // ✅ Use domain constructor
                var entity = new MembershipType(vm.Name);

                _context.MembershipTypes.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                ModelState.AddModelError(string.Empty, $"Database error: {innerMessage}");
                return View("Create and Edit", vm);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(vm.Name), ex.Message);
                return View("Create and Edit", vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // ------------------- EDIT -------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _context.MembershipTypes.Find(id);
            if (entity == null) return NotFound();

            var vm = new MembershipTypeViewModel
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };

            return View("Create and Edit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MembershipTypeViewModel vm)
        {
            if (!ModelState.IsValid) return View("Create and Edit", vm);

            var entity = _context.MembershipTypes.Find(vm.MembershipTypeID);
            if (entity == null) return NotFound();

            try
            {
                // ✅ Use domain method to update name
                entity.UpdateDetails(vm.Name);

                // ✅ Use domain method or constructor logic for IsActive
                if (!vm.IsActive && entity.IsActive)
                {
                    entity.Deactivate();
                }

                _context.SaveChanges();
            }
            catch (ArgumentException ex)
            {
                // Validation error from domain logic
                ModelState.AddModelError(nameof(vm.Name), ex.Message);
                return View("Create and Edit", vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // ------------------- DETAILS -------------------
        public IActionResult Details(int id)
        {
            var entity = _context.MembershipTypes.Find(id);
            if (entity == null) return NotFound();

            var vm = new MembershipTypeViewModel
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };

            return View(vm);
        }

        // ------------------- DELETE -------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var entity = _context.MembershipTypes.Find(id);
            if (entity == null) return NotFound();

            var vm = new MembershipTypeViewModel
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entity = _context.MembershipTypes.Find(id);
            if (entity == null) return NotFound();

            _context.MembershipTypes.Remove(entity);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}