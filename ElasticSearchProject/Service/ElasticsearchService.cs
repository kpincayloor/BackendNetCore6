using ElasticSearchProject.Interface;
using ElasticSearchProject.Mapping;
using ElasticSearchProject.Entity;
using Microsoft.Extensions.Configuration;
using Nest;

namespace ElasticSearchProject.Service
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _client;
        public ElasticsearchService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = CreateInstance();
        }
        private ElasticClient CreateInstance()
        {
            string host = _configuration.GetSection("ElasticConfiguration:Host").Value;
            string port = _configuration.GetSection("ElasticConfiguration:Port").Value;
            string username = _configuration.GetSection("ElasticConfiguration:Username").Value;
            string password = _configuration.GetSection("ElasticConfiguration:Password").Value;
            var settings = new ConnectionSettings(new Uri(host + ":" + port));
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                settings.BasicAuthentication(username, password);

            return new ElasticClient(settings);
        }
        public async Task ChekIndex(string indexName)
        {
            var anyy = await _client.Indices.ExistsAsync(indexName);
            if (anyy.Exists)
                return;

            var response = await _client.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .PermissionMapping()
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );

            return;
        }
        public async Task DeleteIndex(string indexName)
        {
            await _client.Indices.DeleteAsync(indexName);
        }
        public async Task<PermissionEntity> GetDocument(string indexName, string id)
        {
            var response = await _client.GetAsync<PermissionEntity>(id, q => q.Index(indexName));
            return response.Source;
        }
        public async Task<List<PermissionEntity>> GetDocuments(string indexName)
        {
            #region AnalyzeWildcard like sorgusu mantıgında çalışmakta
            var response = await _client.SearchAsync<PermissionEntity>(s => s
                                  .Index(indexName)
                                        .Query(q => q
                                .QueryString(qs => qs
                                .AnalyzeWildcard()
                                   .Query("*" + "iz" + "*")
                                   .Fields(fs => fs
                                       .Fields(f1 => f1.NombreEmpleado
                                               )

                                ))));
            #endregion         

            return response.Documents.ToList();
        }
        public async Task InsertDocument(string indexName, PermissionEntity permission)
        {
            var response = await _client.CreateAsync(permission, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.UpdateAsync<PermissionEntity>(permission.Id, a => a.Index(indexName).Doc(permission));
            }
        }
        public async Task InsertBulkDocuments(string indexName, List<PermissionEntity> permission)
        {
            await _client.IndexManyAsync(permission, index: indexName);
        }
        public async Task DeleteByIdDocument(string indexName, PermissionEntity permission)
        {
            var response = await _client.CreateAsync(permission, q => q.Index(indexName));
            if (response.ApiCall?.HttpStatusCode == 409)
            {
                await _client.DeleteAsync(DocumentPath<PermissionEntity>.Id(permission.Id).Index(indexName));
            }
        }
    }
}
