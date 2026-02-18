using MemberManagement.Domain.Entities;
using MemberManagement.Web.ViewModels.BranchVM;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MemberManagement.Web.Mappers
{
    public static class BranchMapper
    {
        public static BranchItemVM ToViewModel(this Branch branch)
        {
            if (branch == null) return null!;

            return new BranchItemVM
            {
                Id = branch.BranchID,          // map BranchID -> Id
                Name = branch.Name,
                Address = branch.Location,     // map Location -> Address
                IsActive = branch.IsActive,
                DateCreated = DateTime.Now     // placeholder
            };
        }

        public static List<BranchItemVM> ToViewModel(this IEnumerable<Branch> branches)
        {
            return branches.Select(b => b.ToViewModel()).ToList();
        }
    }
}
