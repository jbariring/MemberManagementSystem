using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly MMSDbContext _context;

        public BranchRepository(MMSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Branch branch)
        {
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _context.Branches
                                 .Include(b => b.Members) // optional: include members
                                 .FirstOrDefaultAsync(b => b.BranchID == id);
        }

        public async Task UpdateAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
        }
    }
}
