namespace ReservationSystem.BL.Dots;

public record DepartmentWithTicketsReadDto
{
    public required int Id { get; init; }
    public required string Name { get; init; } = string.Empty;

    public required List<TicketChildReadDto> Tickets { get; init; } = new();
}
