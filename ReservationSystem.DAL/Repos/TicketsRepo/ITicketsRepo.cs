

namespace ReservationSystem.DAL;

public interface ITicketsRepo : IGenericRepo<Ticket>
{
    List<Ticket> GetTicketsByDepartmentId(int departmentId);
}
