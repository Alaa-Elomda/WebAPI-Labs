namespace ReservationSystem.BL.Dots;

public record TicketChildReadDto
{
    public int Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public int DevelopersCount { get; init; }
}
