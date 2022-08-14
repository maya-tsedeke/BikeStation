using System.Linq.Expressions;

namespace Project.ApplicationCore.Interfaces
{
    public interface IReadBaseRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    }
}
