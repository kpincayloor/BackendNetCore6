using Nest;
using ElasticSearchProject.Entity;

namespace ElasticSearchProject.Mapping
{
    public static class Mapping
    {
        public static CreateIndexDescriptor PermissionMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<PermissionEntity>(m => m.Properties(p => p
                .Keyword(k => k.Name(n => n.Id))
                .Text(t => t.Name(n => n.NombreEmpleado))
                .Text(t => t.Name(n => n.ApellidoEmpleado))
                .Number(t => t.Name(n => n.TipoPermisoId))                
                .Date(t => t.Name(n => n.FechaPermiso)))
            );
        }
    }
}
