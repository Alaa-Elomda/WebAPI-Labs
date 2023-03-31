namespace ReservationSystem.DAL;

public interface IDepartmentsRepo : IGenericRepo<Department>
{
    List<Department> GetDepartmentsByName(string name);
}
