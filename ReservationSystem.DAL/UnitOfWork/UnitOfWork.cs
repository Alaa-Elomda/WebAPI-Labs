namespace ReservationSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    public ITicketsRepo TicketsRepo { get;}

    public IDepartmentsRepo DepartmentsRepo { get;}

    public UnitOfWork (ITicketsRepo ticketsRepo, IDepartmentsRepo departmentsRepo)
    {
        TicketsRepo = ticketsRepo;
        DepartmentsRepo = departmentsRepo;
    }

}
