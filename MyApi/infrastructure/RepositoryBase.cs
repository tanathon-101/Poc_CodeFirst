


using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected EmployeeContext _dbcontext { get; set; } 
        public RepositoryBase(EmployeeContext dbcontext) 
        {
            _dbcontext = dbcontext; 
        }
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> FindById(params object[] keyValues)
        {
            return await _dbcontext.Set<T>().FindAsync(keyValues);
        }

        public async Task<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return await _dbcontext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public IQueryable<T> FindAll() => _dbcontext.Set<T>().AsNoTracking();
        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbcontext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }
        public async Task CreateAsync(T entity) => await _dbcontext.Set<T>().AddAsync(entity);
        public void Update(T entity) => _dbcontext.Set<T>().Update(entity);
        public void Delete(T entity) => _dbcontext.Set<T>().Remove(entity);
        public async Task SaveChangesAsync()
        {
        await _dbcontext.SaveChangesAsync();
        }


}