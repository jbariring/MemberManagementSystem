using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.DTOs.Member
{
    public class CreateMemberRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }
        public string? Branch { get; set; }

        [Phone]
        public string? ContactNo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
