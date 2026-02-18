using MemberManagement.Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels
{
    public class MemberVM
    {
        [Key]
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Branch { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
