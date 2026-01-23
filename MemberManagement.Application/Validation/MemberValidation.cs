
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Application.Validation
{
    public class BirthDateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            DateTime birthDate = (DateTime)value;
            return birthDate <= DateTime.Now;
        }
    }

    public class MemberValidation
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [BirthDateNotInFuture(ErrorMessage = "Birth Date cannot be in the future")]
        public DateTime? BirthDate { get; set; }
    }
}
