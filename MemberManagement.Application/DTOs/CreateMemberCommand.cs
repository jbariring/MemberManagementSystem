namespace MemberManagement.Application.DTOs
{
    public record CreateMemberCommand(
        string FirstName,
        string LastName,
        DateTime? BirthDate,
        string? Address,
        string? Branch,
        string? ContactNo,
        string? Email
    );
}
