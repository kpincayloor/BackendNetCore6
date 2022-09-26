using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(permission => permission.Id);

            builder.Property(permission => permission.Id).ValueGeneratedOnAdd();

            builder.Property(permissionType => permissionType.NombreEmpleado).IsRequired();

            builder.Property(permissionType => permissionType.ApellidoEmpleado).IsRequired();

            builder.Property(permissionType => permissionType.TipoPermisoId).IsRequired();

            builder.Property(permissionType => permissionType.FechaPermiso).IsRequired();

            builder.Property(permissionType => permissionType.NombreEmpleado).HasComment("Employee Forename");

            builder.Property(permissionType => permissionType.ApellidoEmpleado).HasComment("Employee Surname");

            builder.Property(permissionType => permissionType.TipoPermisoId).HasComment("Permission Type");

            builder.Property(permissionType => permissionType.FechaPermiso).HasComment("Permission granted on Date");

            builder.HasOne(entity => entity.TipoPermiso)
                .WithMany(entity => entity.Permission)
                .HasForeignKey(entity => entity.TipoPermisoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
