using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;

namespace MemberManagement.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member?> GetByIdAsync(int memberID)
        {
            var members = await _memberRepository.GetAsync(memberID);
            return members.FirstOrDefault();
        }

        public async Task AddAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
        }

        public async Task UpdateAsync(Member member)
        {
            await _memberRepository.UpdateAsync(member);
        }

        public async Task DeleteAsync(int memberID)
        {
            await _memberRepository.DeleteAsync(memberID);
        }
    }
}
