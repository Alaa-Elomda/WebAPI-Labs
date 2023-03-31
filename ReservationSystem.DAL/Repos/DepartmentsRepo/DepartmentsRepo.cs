

namespace ReservationSystem.DAL;

public class DepartmentsRepo : GenericRepo<Department>, IDepartmentsRepo
{
    private readonly ReservationSystemContext _context;

    public DepartmentsRepo(ReservationSystemContext context) : base(context)
    {
        _context = context;
    }

    public List<Department> GetDepartmentsByName(string name)
    {
        return _context.Departments
            .Where(d => d.Name == name)
            .ToList();
    }
}
