using MemberManagement.Domain.Entities;

namespace MemberManagement.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync(); //Active Member
        Task<List<Member>> GetAsync(int memberID);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int memberID);
    }
}
