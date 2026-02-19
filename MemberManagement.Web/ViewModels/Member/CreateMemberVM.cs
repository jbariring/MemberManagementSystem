using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels.Member
{
    public class CreateMemberVM
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }
        public int BranchID { get; set; }  // <--- must exist
        public int MembershipTypeID { get; set; }  // <--- must exist
        // For dropdown
        public IEnumerable<SelectListItem> Branches { get; set; } = new List<SelectListItem>();

        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
