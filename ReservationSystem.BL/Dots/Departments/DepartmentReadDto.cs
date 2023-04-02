namespace ReservationSystem.BL.Dots;

public record DepartmentReadDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
