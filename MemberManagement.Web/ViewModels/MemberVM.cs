using MemberManagement.Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels
{
    public class MemberVM
    {
        [Key]
        public int MemberID { get; set; }

        public string FirstName { get; set; }  // must be set before saving
        public string LastName { get; set; }   // must be set before saving

        // Optional, but validated by attribute
        [BirthDateNotInFuture(ErrorMessage = "Birth Date cannot be in the future")]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }
        public string? Branch { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true; // default to true
        public DateTime DateCreated { get; set; } = DateTime.Now; // auto-set
    }
}
