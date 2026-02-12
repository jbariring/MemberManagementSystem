using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MemberManagement.Domain.Entities;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.MemberID);
        builder.Property(m => m.FirstName).IsRequired();
        builder.Property(m => m.LastName).IsRequired();
    }
}
