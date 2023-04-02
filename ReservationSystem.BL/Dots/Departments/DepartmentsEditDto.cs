namespace ReservationSystem.BL.Dots;

public record DepartmentsEditDto
{
    public required int Id { get; init; }
    public required string Name { get; init; } = string.Empty;

}
