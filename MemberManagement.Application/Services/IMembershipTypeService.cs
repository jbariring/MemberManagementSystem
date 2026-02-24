using MemberManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberManagement.Application.DTOs;

namespace MemberManagement.Application.Services
{
    public interface IMembershipTypeService
    {
        Task<List<MembershipTypeDto>> GetAllAsync();
        Task<MembershipTypeDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateMembershipTypeDto dto);
        Task UpdateAsync(UpdateMembershipTypeDto dto);
        Task DeactivateAsync(int id);
    }
}