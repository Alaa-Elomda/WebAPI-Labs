

using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.DAL;

public class DepartmentsRepo : GenericRepo<Department>, IDepartmentsRepo
{
    private readonly ReservationSystemContext _context;

    public DepartmentsRepo(ReservationSystemContext context) : base(context)
    {
        _context = context;
    }

    public Department? GetByIdWithTickets(int id)
    {
        return _context.Departments
            .Include(d => d.Tickets)
                .ThenInclude(t => t.Developers)
            .FirstOrDefault(d=> d.Id == id);
    }

    public List<Department> GetDepartmentsByName(string name)
    {
        return _context.Departments
            .Where(d => d.Name == name)
            .ToList();
    }
}
