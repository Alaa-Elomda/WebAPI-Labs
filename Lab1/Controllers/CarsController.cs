using Lab1.Filters;
using Lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Lab1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    public static int _requestCount = 0;
    [HttpGet]
    public ActionResult<List<Car>> GetAll()
    {
        return Car.GetCars();
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = Car.GetCars().FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }
        return car;
    }

    [HttpPost]
    [Route("v1")]
    public ActionResult Add(Car car)
    {
        car.Id = new Random().Next(1, 1000); //Assign Random Id for the mobile

        car.Type = "Gas";
        Car.GetCars().Add(car);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = car.Id },
            value: new GeneralResponse("Resource is added"));
    }

    [HttpPost]
    [Route("v2")]
    [ServiceFilter(typeof(ValidateCarTypeAttribute))]
    public ActionResult AddV2(Car car)
    {
        car.Id = new Random().Next(1, 1000); //Assign Random Id for the mobile

        Car.GetCars().Add(car);
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = car.Id },
            value: new GeneralResponse("Resource is added"));
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult Edit(Car car, int id)
    {
        if (id != car.Id)
        {
            return BadRequest(new GeneralResponse("Ids don't match"));
        }

        var carToEedit = Car.GetCars()
            .FirstOrDefault(c => c.Id == id);

        if (carToEedit is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }

        carToEedit.Model = car.Model;
        carToEedit.Property = car.Property;
        carToEedit.Price = car.Price;
        carToEedit.ProductionDate = car.ProductionDate;
        carToEedit.Type = car.Type;

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public ActionResult Delete(int id)
    {
        var carToDelete = Car.GetCars()
            .FirstOrDefault(c => c.Id == id);

        if (carToDelete is null)
        {
            return NotFound(new GeneralResponse("Resource is missing"));
        }

        Car.GetCars().Remove(carToDelete);

        return NoContent();
    }

    [HttpGet]
    [Route("requestCount")]
    public int GetRequestCount()
    {
        return RequestCounter.Count;
    }

}
