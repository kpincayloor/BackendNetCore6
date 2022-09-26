using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var implementedConfigTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(type => !type.IsAbstract
                                        && !type.IsGenericTypeDefinition
                                        && type.GetTypeInfo().ImplementedInterfaces
                                        .Any(implementedInterfaces => implementedInterfaces.GetTypeInfo().IsGenericType
                                        && implementedInterfaces.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configInfo in implementedConfigTypes)
            {                
                dynamic mapInstance = Activator.CreateInstance(configInfo);
                modelBuilder.ApplyConfiguration(mapInstance);
            }
        }
    }
}
