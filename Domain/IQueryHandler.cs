using DataAccess.Entity;

namespace Domain
{
    public interface IQueryHandler<T> where T : class
    {
        IQueryable<Permission> GetPermissions();        
    }
}
