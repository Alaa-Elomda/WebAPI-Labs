using ReservationSystem.BL.Dots;

namespace ReservationSystem.BL;

public interface ITicketsManager
{
    List<TicketReadDto> GetALl();
    void Add(TicketAddDto department);
}
