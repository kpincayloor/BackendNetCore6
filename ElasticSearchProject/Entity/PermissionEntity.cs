namespace ElasticSearchProject.Entity
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public PermissionType? TipoPermiso { get; set; }
        public DateTimeOffset FechaPermiso { get; set; }
    }
}
