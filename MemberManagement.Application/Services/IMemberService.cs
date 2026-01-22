using MemberManagement.Domain.Entities;

namespace MemberManagement.Application.Services
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllAsync();
        Task<Member?> GetByIdAsync(int memberID);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int memberID);
    }
}
