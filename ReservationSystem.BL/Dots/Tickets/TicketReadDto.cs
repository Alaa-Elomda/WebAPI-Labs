using ReservationSystem.DAL;

namespace ReservationSystem.BL.Dots;

public class TicketReadDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public Severity? Severity { get; set; }
    public decimal EstimationCost { get; set; }

    public int? DepartmentId { get; set; }
}
