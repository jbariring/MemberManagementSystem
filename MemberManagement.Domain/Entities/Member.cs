using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Domain.Entities
{
    public class Member
    {
        // EF Core requires a parameterless constructor
        protected Member() { }

        // Primary Key
        public int MemberID { get; private set; }

        // Required data
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }

        // Optional data
        public string? Address { get; private set; }

        // Foreign Key to Branch
        public int BranchID { get; private set; }
        public int MembershipTypeID { get; private set; }  // <-- New FK


        // Navigation property
        public Branch Branch { get; private set; }
        public MembershipType MembershipType { get; private set; } // <-- New navigation property

        public string? ContactNo { get; private set; }
        public string? Email { get; private set; }

        // System data
        public bool IsActive { get; private set; } // Active flag for soft delete / activation
        public DateTime DateCreated { get; private set; } // Timestamp when member was created

        // Constructor to create a valid member
        public Member(
            string firstName,
            string lastName,
            DateTime? birthDate,
            string? address,
            int branchId,
            int membershipTypeId,
            string? contactNo,
            string? email
        )
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            BranchID = branchId;
            MembershipTypeID = membershipTypeId;
            ContactNo = contactNo;
            Email = email;

            // By default, new members are active
            IsActive = true;
            DateCreated = DateTime.UtcNow;
        }

        public Member(string firstName, string lastName, DateTime? birthDate, string? address, Branch branchEntity, string? contactNo, string? email)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            ContactNo = contactNo;
            Email = email;
        }

        // Update member details
        public void UpdateDetails(
            string firstName,
            string lastName,
            DateTime? birthDate,
            string? address,
            int branchId,
            int membershipTypeId,
            string? contactNo,
            string? email
        )
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Address = address;
            BranchID = branchId;
            MembershipTypeID = membershipTypeId;
            ContactNo = contactNo;
            Email = email;
        }

        // Deactivate member (soft delete)
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
