using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MemberManagement.Web.ViewModels
{
    public class MemberListVM
    {
        public List<MemberVM> Members { get; set; } = new List<MemberVM>();

        public string? SearchLastName { get; set; }
        public string? Branch { get; set; }
        public SelectList Branches { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalMembers { get; set; }
        public int StartItem { get; set; }
        public int EndItem { get; set; }
    }
}
