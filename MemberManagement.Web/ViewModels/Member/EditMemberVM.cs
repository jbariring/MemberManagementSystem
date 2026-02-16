using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels.Member
{
    public class EditMemberVM
    {
        [Required]
        public int MemberID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }
        public int BranchID { get; set; }

        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public IEnumerable<SelectListItem> Branches { get; set; } = new List<SelectListItem>();
    }
}
