using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels
{
    public class MembershipTypeViewModel
    {
        public int MembershipTypeID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}