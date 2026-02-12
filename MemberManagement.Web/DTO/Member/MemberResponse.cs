namespace MemberManagement.Web.DTOs.Member
{
    public class MemberResponse
    {
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
