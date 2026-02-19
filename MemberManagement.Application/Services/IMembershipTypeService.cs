using MemberManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberManagement.Application.Services
{
    public interface IMembershipTypeService
    {
        Task<MembershipType> GetByIdAsync(int id);
        Task<List<MembershipType>> GetAllAsync();
        Task AddAsync(MembershipType membershipType);
        Task UpdateAsync(MembershipType membershipType);
        Task SaveChangesAsync();
    }
}