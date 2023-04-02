using ReservationSystem.BL.Dots;
using ReservationSystem.DAL;

namespace ReservationSystem.BL;

public class DepartmentsManager : IDepartmentsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<DepartmentReadDto> GetAll()
    {
        List<Department> departmentsFromDb = _unitOfWork.DepartmentsRepo.GetAll();

        return departmentsFromDb
            .Select(d => new DepartmentReadDto
            {
                Id= d.Id,
                Name = d.Name,

            })
            .ToList();
    }
    public int Add(DepartmentAddDto departmentDto)
    {
        var department = new Department
        {
            Name = departmentDto.Name,
        };

        _unitOfWork.DepartmentsRepo.Add(department);
        _unitOfWork.DepartmentsRepo.SaveChanges();

        return department.Id;
    }



    public DepartmentWithTicketsReadDto? GetByIdWithTickets(int id)
    {
        Department? department = _unitOfWork.DepartmentsRepo.GetByIdWithTickets(id);

        if (department == null) { return null; }

        return new DepartmentWithTicketsReadDto
        {
            Id = id,
            Name = department.Name,
            Tickets = department.Tickets.Select(t => new TicketChildReadDto
            {
                Id = t.Id,
                Description= t.Description,
                DevelopersCount = t.Developers.Count

            }).ToList()
        };
    }

    public void Edit(int id,DepartmentsEditDto departmentDpo)
    {
        Department? departmentToEdit = _unitOfWork.DepartmentsRepo.GetById(id);
        if (departmentToEdit == null) { return; }

        departmentToEdit.Name= departmentDpo.Name;

        _unitOfWork.DepartmentsRepo.Update(departmentToEdit);
        _unitOfWork.DepartmentsRepo.SaveChanges();

    }


    public void Delete(int id)
    {
        Department? department = _unitOfWork.DepartmentsRepo.GetById(id);

        if(department is null) { return; }

        _unitOfWork.DepartmentsRepo.Delete(department);
        _unitOfWork.DepartmentsRepo.SaveChanges();
    }

}
