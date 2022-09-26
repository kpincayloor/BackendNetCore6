using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Permission>? Permission { get; set; }

    }
}
