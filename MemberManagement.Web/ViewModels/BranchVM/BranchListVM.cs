using System;
using System.Collections.Generic;

namespace MemberManagement.Web.ViewModels.Branch
{
    public class BranchListVM
    {
        public List<BranchItemVM> Branches { get; set; } = new List<BranchItemVM>();
    }

    public class BranchItemVM
    {
        public int BranchID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
