using System.Linq.Expressions;

public interface IRepositoryBase<T>
    {
       Task<T> FindById(params object[] keyValues);
       Task<IEnumerable<T>> FindAllAsync();
       IQueryable<T> FindAll();
       Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
       Task CreateAsync(T entity);
       void Update(T entity);
       void Delete(T entity);
       Task<T> FindBy(Expression<Func<T, bool>> expression);
    }