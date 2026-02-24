using MemberManagement.Application.Services;
using MemberManagement.Domain.Entities;
using MemberManagement.Domain.Interfaces;

public class MembershipTypeService : IMembershipTypeService
{
    private readonly IMembershipTypeRepository _repository;

    public MembershipTypeService(IMembershipTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MembershipTypeDto>> GetAllAsync()
    {
        var types = await _repository.GetAllAsync();
        return types.Select(mt => new MembershipTypeDto
        {
            MembershipTypeID = mt.MembershipTypeID,
            Name = mt.Name,
            IsActive = mt.IsActive
        }).ToList();
    }

    public async Task<MembershipTypeDto?> GetByIdAsync(int id)
    {
        var mt = await _repository.GetByIdAsync(id);
        if (mt == null) return null;
        return new MembershipTypeDto
        {
            MembershipTypeID = mt.MembershipTypeID,
            Name = mt.Name,
            IsActive = mt.IsActive
        };
    }

    public async Task CreateAsync(CreateMembershipTypeDto dto)
    {
        var mt = new MembershipType(dto.Name);
        await _repository.AddAsync(mt);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateMembershipTypeDto dto)
    {
        var mt = await _repository.GetByIdAsync(dto.MembershipTypeID);
        if (mt == null) throw new KeyNotFoundException("Membership type not found.");
        mt.UpdateDetails(dto.Name);
        await _repository.UpdateAsync(mt);
        await _repository.SaveChangesAsync();
    }

    public async Task DeactivateAsync(int id)
    {
        var mt = await _repository.GetByIdAsync(id);
        if (mt == null) throw new KeyNotFoundException("Membership type not found.");
        mt.Deactivate();
        await _repository.SaveChangesAsync();
    }
}