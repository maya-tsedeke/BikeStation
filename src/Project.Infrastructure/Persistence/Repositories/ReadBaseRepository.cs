using Project.ApplicationCore.Interfaces;
using Project.Infrastructure.Persistence.Contexts;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class ReadBaseRepository<T> : IReadBaseRepository<T> where T : class
    {
        protected StationsContext StationsContext { get; set; }

        public ReadBaseRepository(StationsContext stationsContext)
        {
            this.StationsContext = stationsContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.StationsContext.Set<T>()
              .AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.StationsContext.Set<T>()
             .Where(expression)
             .AsNoTracking();
        }
    }
}
