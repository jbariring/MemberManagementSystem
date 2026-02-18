using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemberManagement.Application.Services
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch> GetBranchByIdAsync(int id);
        Task CreateBranchAsync(string name, string? address);
        Task UpdateBranchAsync(int id, string name, string? address);
        Task DeleteBranchAsync(int id);
    }

    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task CreateBranchAsync(string name, string? address)
        {
            var branch = new Branch(name, address);
            await _branchRepository.AddAsync(branch);
        }

        public async Task DeleteBranchAsync(int id)
        {
            await _branchRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _branchRepository.GetAllAsync();
        }

        public async Task<Branch> GetBranchByIdAsync(int id)
        {
            return await _branchRepository.GetByIdAsync(id);
        }

        public async Task UpdateBranchAsync(int id, string name, string? address)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch != null)
            {
                branch.UpdateDetails(name, address);
                await _branchRepository.UpdateAsync(branch);
            }
        }
    }
}
