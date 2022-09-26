using DataAccess.Entity;
using DataAccess;
using ElasticSearchProject.Interface;
using ElasticSearchProject.Entity;
using KafkaApacheProject;
using DataAccess.Dtos;

namespace Domain.CommandHandler
{
    public class RequestPermissionCommandHandler : ICommandHandler<Permission>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IElasticsearchService _elasticsearchService;
        private readonly IKafkaMessageBus<string, PermissionDto> _bus;
        public RequestPermissionCommandHandler(IUnitOfWork unitOfWork, IElasticsearchService elasticsearchService, IKafkaMessageBus<string, PermissionDto> bus)
        {
            this._unitOfWork = unitOfWork;
            this._elasticsearchService = elasticsearchService;
            this._bus = bus;
        }
        public async Task<CommandResult> Execute(Permission permission)
        {
            var isPermissionExist = this._unitOfWork.GetRepository<Permission>().FindById(permission.Id);
            if (isPermissionExist == null)
            {
                this._unitOfWork.GetRepository<Permission>().Add(permission);
                this._unitOfWork.Commit();
                var guid = Guid.NewGuid();
                var value = new PermissionDto { Id = guid, Name = "request" };
                var objPermission = new PermissionEntity { Id = permission.Id, NombreEmpleado = permission.NombreEmpleado, ApellidoEmpleado = permission.ApellidoEmpleado, TipoPermisoId = permission.TipoPermisoId, FechaPermiso = permission.FechaPermiso };
                await _elasticsearchService.InsertDocument("permission", objPermission);
                await _bus.PublishAsync("Request", value);
                return new CommandResult { Status = true, Message = "Permiso agregado con éxito!" };
            }

            return new CommandResult { Status = false, Message = "El permiso ya existe." };
        }
        public async Task<CommandResult> ExecuteUpdate(Permission command)
        {
            var permissionId = this._unitOfWork.GetRepository<Permission>().FindById(command.Id);
            if (permissionId == null)
            {
                return new CommandResult { Status = false, Message = "El permiso no existe." };
            }
            permissionId.NombreEmpleado = command.NombreEmpleado;
            permissionId.ApellidoEmpleado = command.ApellidoEmpleado;
            permissionId.TipoPermisoId = command.TipoPermisoId;
            permissionId.FechaPermiso = DateTimeOffset.Now;

            this._unitOfWork.GetRepository<Permission>().Update(permissionId);
            this._unitOfWork.Commit();
            var guid = Guid.NewGuid();
            var value = new PermissionDto { Id = guid, Name = "modify" };
            var objPermission = new PermissionEntity { Id = command.Id, NombreEmpleado = command.NombreEmpleado, ApellidoEmpleado = command.ApellidoEmpleado, TipoPermisoId = command.TipoPermisoId, FechaPermiso = command.FechaPermiso };
            await _elasticsearchService.InsertDocument("permission", objPermission);
            await _bus.PublishAsync("Modify", value);
            return new CommandResult { Status = true, Message = "Permiso actualizado con éxito!" };
        }
    }
}
