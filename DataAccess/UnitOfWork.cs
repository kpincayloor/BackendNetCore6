using DataAccess.Data;
using DataAccess.Dtos;
using DataAccess.Repository;
using ElasticSearchProject.Interface;
using KafkaApacheProject;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext DatabaseContext { get; set; }

        private readonly Dictionary<string, object> repositories = new Dictionary<string, object>();
        
        private IElasticsearchService _elasticsearchService;

        private readonly IKafkaMessageBus<string, PermissionDto> _bus;

        public UnitOfWork(DatabaseContext databaseContext, IElasticsearchService elasticsearchService, IKafkaMessageBus<string, PermissionDto> bus)
        {
            DatabaseContext = databaseContext;
            this._elasticsearchService = elasticsearchService;
            this._bus = bus;
        }
        public void Commit()
        {
            DatabaseContext.SaveChanges();
            repositories.Clear();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            string typeName = typeof(T).Name;
            if (repositories.Keys.Contains(typeName))
            {
                return repositories[typeName] as IRepository<T>;
            }
            IRepository<T> newRepository = new Repository<T>(DatabaseContext, _elasticsearchService, _bus);

            repositories.Add(typeName, newRepository);
            return newRepository;
        }
    }
}
