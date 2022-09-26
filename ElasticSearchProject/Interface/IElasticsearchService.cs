using ElasticSearchProject.Entity;

namespace ElasticSearchProject.Interface
{
    public interface IElasticsearchService
    {
        Task ChekIndex(string indexName);
        Task InsertDocument(string indexName, PermissionEntity permission);
        Task DeleteIndex(string indexName);
        Task DeleteByIdDocument(string indexName, PermissionEntity permission);
        Task InsertBulkDocuments(string indexName, List<PermissionEntity> permission);
        Task<PermissionEntity> GetDocument(string indexName, string id);
        Task<List<PermissionEntity>> GetDocuments(string indexName);
    }
}
