using Microsoft.AspNetCore.Mvc.Rendering;

namespace MemberManagement.Web.ViewModels
{
    public class MemberListVM
    {
        public IEnumerable<MemberVM> Members { get; set; }

        public string SearchLastName { get; set; }
        public string Branch { get; set; }

        public SelectList Branches { get; set; } // keep only this

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public int TotalMembers { get; set; }
        public int StartItem { get; set; }
        public int EndItem { get; set; }


    }
}
