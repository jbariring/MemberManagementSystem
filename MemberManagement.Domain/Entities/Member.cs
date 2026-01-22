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
//Validation Attribute to ensure BirthDate is not in the future
public class BirthDateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Required should handle null

            DateTime birthDate = (DateTime)value;

            // BirthDate must not be in the future
            return birthDate <= DateTime.Now;
        }
    }

