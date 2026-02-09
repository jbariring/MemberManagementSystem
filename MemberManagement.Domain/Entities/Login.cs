using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Domain.Entities
{
    public class AppUser
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } // store hashed password

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
