using ReservationSystem.BL.Dots;
using ReservationSystem.DAL;

namespace ReservationSystem.BL;

public class TicketsManager : ITicketsManager
{
    private readonly ITicketsRepo _ticketsRepo;

    public TicketsManager(ITicketsRepo ticketsRepo)
    {
 
        _ticketsRepo = ticketsRepo;
    }

    public List<TicketReadDto> GetALl()
    {
        List<Ticket> ticketsFromDb = _ticketsRepo.GetAll();

        return ticketsFromDb
            .Select(d => new TicketReadDto
            {
                Id= d.Id,
                Description= d.Description,
                Severity= d.Severity,
                EstimationCost= d.EstimationCost,
                DepartmentId= d.DepartmentId

            })
            .ToList();
    }
    public void Add(TicketAddDto ticketDto)
    {
        var ticket = new Ticket
        {
            Description= ticketDto.Description,
            Severity= ticketDto.Severity,
            EstimationCost = ticketDto.EstimationCost
            
        };

        _ticketsRepo.Add(ticket);
        _ticketsRepo.SaveChanges();
    }


}
