using System.ComponentModel.DataAnnotations;

namespace MemberManagement.Web.ViewModels.Branch
{
    public class CreateBranchVM
    {
        [Required(ErrorMessage = "Branch name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
    }
}
