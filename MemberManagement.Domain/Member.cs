using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace MemberManagement.Domain
{
    public class Member
    {

        //PK
        [Key]
        public int MemberID { get; set; }

        //Member Data
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
