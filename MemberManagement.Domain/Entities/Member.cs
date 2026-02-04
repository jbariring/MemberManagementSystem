using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace MemberManagement.Domain.Entities
{
    public class Member
    {
        //PK
        [Key]
        public int MemberID { get; set; }

        //Member Data
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        //OptionalData

        public string? Address { get; set; }
        public string? Branch { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }

        //System Data
        public bool IsActive { get; set; } = true; // default to true
        public DateTime DateCreated { get; set; } = DateTime.Now; // auto-set

    }
}
