public class MembershipTypeDto
{
    public int MembershipTypeID { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMembershipTypeDto
{
    public string Name { get; set; }
}

public class UpdateMembershipTypeDto
{
    public int MembershipTypeID { get; set; }
    public string Name { get; set; }
}
