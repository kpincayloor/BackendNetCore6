using DataAccess;
using DataAccess.Entity;

namespace Domain.QueryHandler
{
    public class GetPermissionQueryHandler : IQueryHandler<Permission>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IQueryable<Permission> GetPermissions()
        {
            return _unitOfWork.GetRepository<Permission>().GetAllAsQueryable();
        }
    }
}
