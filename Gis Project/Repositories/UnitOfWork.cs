using Gis_Project.Context;

namespace Gis_Project.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GisContext _context;
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(GisContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var typeName = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryInstance = new GenericRepository<TEntity, TKey>(_context);
                _repositories[typeName] = repositoryInstance;
            }

            return (IGenericRepository<TEntity, TKey>)_repositories[typeName]!;
        }
    }
}
