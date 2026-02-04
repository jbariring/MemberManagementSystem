
using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Application.Validation
{
    public class BirthDateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) // optional field
                return true;

            if (value is DateTime date)
            {
                if (date == DateTime.MinValue) // ignore empty input
                    return true;

                return date <= DateTime.Today; // must not be future
            }

            return false;
        }


        public class MemberValidation
        {
            [Required(ErrorMessage = "First Name is required")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required")]
            public string LastName { get; set; }

            [BirthDateNotInFuture(ErrorMessage = "Birth Date cannot be in the future")]
            public DateTime? BirthDate { get; set; }

        }
    }
}




