public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeContext dbcontext) : base(dbcontext)
    {
    }
}