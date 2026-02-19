namespace MemberManagement.Domain.Entities
{
    public class MembershipType
    {
        protected MembershipType() { }

        public int MembershipTypeID { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<Member> Members { get; private set; } = new List<Member>();

        public MembershipType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Membership type name cannot be empty.");

            Name = name;
            IsActive = true;
        }

        public void UpdateDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Membership type name cannot be empty.");

            Name = name;
        }

        public void Deactivate() => IsActive = false;
    }
}
