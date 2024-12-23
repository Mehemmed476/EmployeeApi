using EmployeeApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeApp.DAL.Configurations;

public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(u => u.LastName)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(u => u.PhoneNumber)
            .IsRequired();
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255); 

        builder.HasCheckConstraint(
            "CHK_User_Email",
            @"Email LIKE '_%@_%._%'"
        );
    }
}