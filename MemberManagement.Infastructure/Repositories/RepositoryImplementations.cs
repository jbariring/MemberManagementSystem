using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MMSDbContext _context;

        public MemberRepository(MMSDbContext context)
        {
            _context = context;
        }

        // Get all active members
        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members
                                 .Where(m => m.IsActive)
                                 .ToListAsync();
        }

        // Get member by ID
        public async Task<Member?> GetByIdAsync(int memberId)
        {
            return await _context.Members
                                 .FirstOrDefaultAsync(m => m.MemberID == memberId && m.IsActive);
        }

        // Add a new member
        public async Task AddAsync(Member member)
        {
            // No need to set IsActive/DateCreated; entity constructor handles it
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        // Update member details
        public async Task UpdateAsync(Member member)
        {
            // Entity should have updated values via UpdateDetails
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        // Soft delete a member
        public async Task DeleteAsync(int memberId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                member.Deactivate(); // domain handles IsActive
                _context.Members.Update(member);
                await _context.SaveChangesAsync();
            }
        }

        // Optional: paginated members
        public async Task<List<Member>> GetPagedAsync(int pageNumber, int pageSize, string? searchLastName = null, string? branch = null)
        {
            var query = _context.Members.AsQueryable().Where(m => m.IsActive);

            if (!string.IsNullOrEmpty(searchLastName))
                query = query.Where(m => m.LastName.ToLower().Contains(searchLastName.ToLower()));

            var branchLower = branch.Trim().ToLower();
            query = query.Where(m => m.Branch.Name.ToLower() == branchLower && m.Branch.IsActive);

            return await query
                        .OrderBy(m => m.MemberID)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        // Optional: total count for pagination
        public async Task<int> CountAsync(string? searchLastName = null, string? branch = null)
        {
            var query = _context.Members.AsQueryable().Where(m => m.IsActive);

            if (!string.IsNullOrEmpty(searchLastName))
                query = query.Where(m => m.LastName.ToLower().Contains(searchLastName.ToLower()));

            if (!string.IsNullOrEmpty(branch))
                query = query.Where(m => m.Branch.Name == branch && m.Branch.IsActive);

            return await query.CountAsync();
        }
    }
}
