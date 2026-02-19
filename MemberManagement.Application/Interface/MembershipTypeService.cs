using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberManagement.Application.Interface
{
    public class MembershipTypeService
    {
        private readonly IMembershipTypeRepository _repository;

        public MembershipTypeService(IMembershipTypeRepository repository)
        {
            _repository = repository;
        }

        // Create
        public async Task<MembershipTypeDto> CreateAsync(CreateMembershipTypeDto dto)
        {
            var entity = new MembershipType(dto.Name);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return new MembershipTypeDto
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };
        }

        // Update
        public async Task<MembershipTypeDto> UpdateAsync(UpdateMembershipTypeDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.MembershipTypeID);
            if (entity == null) throw new Exception("Membership Type not found.");

            entity.UpdateDetails(dto.Name);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return new MembershipTypeDto
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };
        }

        // Deactivate
        public async Task DeactivateAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Membership Type not found.");

            entity.Deactivate();
            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();
        }

        // Get All
        public async Task<List<MembershipTypeDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new MembershipTypeDto
            {
                MembershipTypeID = e.MembershipTypeID,
                Name = e.Name,
                IsActive = e.IsActive
            }).ToList();
        }

        // Get by Id
        public async Task<MembershipTypeDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Membership Type not found.");

            return new MembershipTypeDto
            {
                MembershipTypeID = entity.MembershipTypeID,
                Name = entity.Name,
                IsActive = entity.IsActive
            };
        }
    }
}

