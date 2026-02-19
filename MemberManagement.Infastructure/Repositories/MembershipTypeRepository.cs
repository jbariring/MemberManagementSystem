using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using MemberManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.Infrastructure.Repositories
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly MMSDbContext _context;

        public MembershipTypeRepository(MMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MembershipType membershipType)
        {
            await _context.MembershipTypes.AddAsync(membershipType);
        }

        public async Task<List<MembershipType>> GetAllAsync()
        {
            return await _context.MembershipTypes.ToListAsync();
        }

        public async Task<MembershipType> GetByIdAsync(int id)
        {
            return await _context.MembershipTypes
                .FirstOrDefaultAsync(mt => mt.MembershipTypeID == id);
        }

        public Task UpdateAsync(MembershipType membershipType)
        {
            _context.MembershipTypes.Update(membershipType);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
