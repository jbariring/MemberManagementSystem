using MemberManagement.Application.Services;
using MemberManagement.Domain.Entities;
using MemberManagement.Web.ViewModels.BranchVM;
using MemberManagement.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MemberManagement.Web.Controllers
{
    public class BranchDController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchDController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET: Branch
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllBranchesAsync();

            // Map entities to ViewModel
            var branchVMs = branches.ToViewModel(); // List<BranchItemVM>

            return View(branchVMs); // matches Index.cshtml model
        }

        // GET: Branch/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string? address)
        {
            if (ModelState.IsValid)
            {
                await _branchService.CreateBranchAsync(name, address);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Branch/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null) return NotFound();

            // Map to ViewModel
            var branchVM = branch.ToViewModel();

            return View(branchVM); // matches Edit.cshtml model
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, string? address)
        {
            if (ModelState.IsValid)
            {
                await _branchService.UpdateBranchAsync(id, name, address);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Branch/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _branchService.GetBranchByIdAsync(id);
            if (branch == null) return NotFound();

            // Map to ViewModel
            var branchVM = branch.ToViewModel();

            return View(branchVM); // matches Delete.cshtml model
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _branchService.DeleteBranchAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
