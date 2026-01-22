using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure.Repositories
{
    public class MemberRepository(MMSDbContext context) : IMemberRepository
    {
        private readonly MMSDbContext _context = context;

        public async Task<List<Member>> GetAllAsync() // Get all active members
        {
            return await _context.Members.Where(m => m.IsActive).ToListAsync();
        }

        public async Task<List<Member>> GetAsync(int memberID) //Get member by ID
        {
            return await _context.Members.Where(m => m.MemberID == memberID).ToListAsync();
        }

        public async Task AddAsync(Member member) // Add a new member
        {
            member.IsActive = true;
            member.DateCreated = DateTime.UtcNow;
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Member member) // Update other fields as necessary
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int memberID) // Soft delete a member
        {
            var member = await _context.Members.FindAsync(memberID);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}



