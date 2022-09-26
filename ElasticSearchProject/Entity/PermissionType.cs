namespace ElasticSearchProject.Entity
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public ICollection<PermissionEntity>? Permission { get; set; }

    }
}
