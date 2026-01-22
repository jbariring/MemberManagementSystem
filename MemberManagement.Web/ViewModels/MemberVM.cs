using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.Models
{
    public class MemberVM
    {
        //PK
        [Key]
        public int MemberID { get; set; }

        //Member Data
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [BirthDateNotInFuture(ErrorMessage = "Birth Date cannot be in the future")]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string Branch { get; set; }

        //Additional Field
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
}
