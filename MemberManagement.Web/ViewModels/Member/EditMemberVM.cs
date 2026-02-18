using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels.Member
{
    public class EditMemberVM
    {
        public int MemberID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Please select a branch")]
        public int? BranchID { get; set; }  // <-- important for dropdown

        public string? ContactNo { get; set; }
        public string? Email { get; set; }

        public List<SelectListItem> Branches { get; set; } = new List<SelectListItem>();
    }
}
