using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.HasKey(permissionType => permissionType.Id);

            builder.Property(permissionType => permissionType.Id).ValueGeneratedOnAdd();

            builder.Property(permissionType => permissionType.Descripcion).IsRequired();

            builder.Property(permissionType => permissionType.Descripcion).HasComment("Permission description");

            builder.HasData(
                new PermissionType
                {
                    Id = 1,
                    Descripcion = "Admin"
                },
                new PermissionType
                {
                    Id = 2,
                    Descripcion = "User"
                }
                );

        }
    }
}
