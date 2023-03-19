using Lab1.Validations;
using System.Reflection;

namespace Lab1.Models;

public class Car
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Property { get; set; } = string.Empty;
    public double Price { get; set; }

    [DateInPast]
    public DateTime ProductionDate { get; set; }
    public string Type { get; set; } = string.Empty;

    //public Car(int id,
    //string model,
    //string property,
    //double price,
    //DateTime productionDate)
    //{
    //    Id = id;
    //    Model = model;
    //    Property = property;
    //    Price = price;
    //    ProductionDate = productionDate;
    //}

    private static List<Car> _cars = new List<Car>{};

    public static List<Car> GetCars() => _cars;


}
