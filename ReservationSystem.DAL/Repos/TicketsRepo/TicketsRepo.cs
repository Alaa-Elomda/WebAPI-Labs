namespace ReservationSystem.DAL;

public class TicketsRepo : GenericRepo<Ticket> , ITicketsRepo
{
    private readonly ReservationSystemContext _context;

    public TicketsRepo(ReservationSystemContext context) : base(context)
    {
        _context = context;
    }

    public List<Ticket> GetTicketsByDepartmentId(int departmentId)
    {
        return _context.Tickets
            .Where(t=>t.DepartmentId== departmentId)
            .ToList();
            }
}
