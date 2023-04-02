using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.BL;
using ReservationSystem.BL.Dots;
using ReservationSystem.DAL;
using System.Net.Sockets;

namespace ReservationSystem.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentsManager _departmentsManager;

    public DepartmentsController(IDepartmentsManager departmentsManager) 
    {
        _departmentsManager = departmentsManager;
    }

    [HttpGet]
    public ActionResult<List<DepartmentReadDto>> GetAll()
    {
        return _departmentsManager.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<DepartmentWithTicketsReadDto> GetByIdWithTickets(int id)
    {
        var departmentDto = _departmentsManager.GetByIdWithTickets(id);
        if (departmentDto is null)
        {
            return NotFound(new { Message = "No Department Found!!" });
        }
        return departmentDto;
    }

    [HttpPost]
    public ActionResult Add(DepartmentAddDto departmentDto)
    {
        _departmentsManager.Add(departmentDto);
        return CreatedAtAction(
            actionName: nameof(GetAll),
            value: "Added Successfully");
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Edit(int id,DepartmentsEditDto departmentDpo)
    {
        if (departmentDpo.Id != id) return NotFound(new { Message = "No Department Found!!" });

        _departmentsManager.Edit(id,departmentDpo);
        return CreatedAtAction(
            actionName: nameof(GetAll),
            value: "Updated Successfully");
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(int id, DepartmentsEditDto departmentDpo)
    {
        if (departmentDpo.Id != id) return NotFound(new { Message = "No Department Found!!" });

        _departmentsManager.Delete(id);
        return CreatedAtAction(
            actionName: nameof(GetAll),
            value: "Deleted Successfully");

    }
}
