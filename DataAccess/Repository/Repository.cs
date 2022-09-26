using System.Linq.Expressions;
using DataAccess.Data;
using ElasticSearchProject.Interface;
using KafkaApacheProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using DataAccess.Dtos;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> objectSet;
        private readonly DatabaseContext context;
        private IElasticsearchService _elasticsearchService;
        private readonly IKafkaMessageBus<string, PermissionDto> _bus;
        public Repository(DatabaseContext context, IElasticsearchService elasticsearchService, IKafkaMessageBus<string, PermissionDto> bus)
        {
            objectSet = context.Set<T>();
            this.context = context;
            this._elasticsearchService = elasticsearchService;
            this._bus = bus;
        }
        public void Add(T entity)
        {
            objectSet.Add(entity);
        }

        public void AddRange(ICollection<T> entities)
        {
            objectSet.AddRange(entities);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties == null || !includeProperties.Any())
            {
                return objectSet.Where(predicate);
            }

            IQueryable<T> queryable = includeProperties.Aggregate(objectSet.AsQueryable(), (current, includeProperty) => current.Include(includeProperty));

            return queryable.Where(predicate);
        }

        public T FindById(params object[] keyValues)
        {
            return objectSet.Find(keyValues);
        }

        public IQueryable<T> GetAllAsQueryable(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            IQueryable<T> queryable = objectSet.AsQueryable<T>();
            return include(queryable).AsNoTracking();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            var guid = Guid.NewGuid();
            var value = new PermissionDto { Id = guid, Name = "get" };
            _bus.PublishAsync("Get", value);
            return objectSet.AsQueryable<T>();
        }

        public IQueryable<T> GetAllTypeAsQueryable()
        {
            return objectSet.AsQueryable<T>();
        }
        public void Update(T entity)
        {
            objectSet.Update(entity);
        }

        public void UpdateRange(ICollection<T> entities)
        {
            objectSet.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            objectSet.Remove(entity);
        }

        public void RemoveRange(ICollection<T> entities)
        {
            objectSet.RemoveRange(entities);
        }
    }
}
