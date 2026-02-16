namespace MemberManagement.Domain.Entities
{
    public class Branch
    {
        // EF Core requires a parameterless constructor
        protected Branch() { }

        // Primary Key
        public int BranchID { get; private set; }

        // Branch details
        public string Name { get; private set; }
        public string Location { get; private set; }

        // Active flag for soft delete / activation
        public bool IsActive { get; private set; }

        // Navigation property: one branch has many members
        public ICollection<Member> Members { get; private set; } = new List<Member>();

        // Constructor
        public Branch(string name, string location)
        {
            Name = name;
            Location = location;
            IsActive = true; // New branches are active by default
        }

        // Update branch details
        public void UpdateDetails(string name, string location)
        {
            Name = name;
            Location = location;
        }

        // Deactivate branch (soft delete)
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
