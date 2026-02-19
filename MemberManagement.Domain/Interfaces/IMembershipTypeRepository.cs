using MemberManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.Domain.Interfaces
{
    public interface IMembershipTypeRepository
    {
        Task<MembershipType> GetByIdAsync(int id);
        Task<List<MembershipType>> GetAllAsync();
        Task AddAsync(MembershipType membershipType);
        Task UpdateAsync(MembershipType membershipType);
        Task SaveChangesAsync();
    }
}
