using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace MemberManagement.Domain.Entities
{
    public class Member
    {
        // EF Core needs this
        protected Member() { }

        // PK
        public int MemberID { get; private set; }

        // Required data
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public DateTime? BirthDate { get; private set; }

        // Optional data
        public string? Address { get; private set; }
        public string? Branch { get; private set; }
        public string? ContactNo { get; private set; }
        public string? Email { get; private set; }

        // System data
        public bool IsActive { get; private set; }
        public DateTime DateCreated { get; private set; }

        // ✔ Constructor = valid member always
        public Member(
            string firstName,
            string lastName,
            DateTime? birthDate,
            string? address,
            string? branch,
            string? contactNo,
            string? email
        )
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            Branch = branch;
            ContactNo = contactNo;
            Email = email;

            IsActive = true;
            DateCreated = DateTime.UtcNow;
        }

        // ✔ Explicit behavior
        public void UpdateDetails(
            string firstName,
            string lastName,
            DateTime? birthDate,
            string? address,
            string? branch,
            string? contactNo,
            string? email
        )
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            Branch = branch;
            ContactNo = contactNo;
            Email = email;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
