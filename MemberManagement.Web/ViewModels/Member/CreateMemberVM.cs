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
        public string? Branch { get; set; }

        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
