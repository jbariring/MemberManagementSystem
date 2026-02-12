using MemberManagement.Domain.Entities;

namespace MemberManagement.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync(); // Get all active members
        Task<Member?> GetByIdAsync(int memberID); // Get single member by ID
        Task AddAsync(Member member); // Add a new member
        Task UpdateAsync(Member member); // Update member details
        Task DeleteAsync(int memberID); // Soft delete
    }
}
