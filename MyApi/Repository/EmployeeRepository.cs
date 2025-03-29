public class employeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public employeeRepository(EmployeeContext dbcontext) : base(dbcontext)
    {
    }
}