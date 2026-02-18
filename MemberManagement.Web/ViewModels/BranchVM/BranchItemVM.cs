namespace MemberManagement.Web.ViewModels.BranchVM
{
    public class BranchItemVM
    {
        public int Id { get; set; }             // maps from Branch.BranchID
        public string Name { get; set; }
        public string? Address { get; set; }    // maps from Branch.Location
        public bool IsActive { get; set; }      // maps from Branch.IsActive
        public DateTime DateCreated { get; set; } // placeholder
    }
}
