using System.ComponentModel.DataAnnotations;

var car = new Car()
{
    Order = 1,
    Make = "Honda",
    Year = 2001

};

public class Car 
{
    public int Order { get; set; }

    public string Make 
    { 
        get { return Make; } 
        set
        {
            if (value.Length > 30)
            {
                Console.WriteLine("Maximum 30 characters");
            }
            else
            {
                Make = value;
            }
        } 
    }

    public int Year { get; set; }

    public double Price { get; set; }

    public string Currency 
    { 
        get { return Currency; } 
        set 
        {
            if (value != "BGN" || value != "EUR")
            {
                Console.WriteLine($"{value} is not a valid currency, use either BGN or EUR.");
            }
        }
    }
}

public class CarService 
{
    public List<Car> Cars { get; set; }
    
    public List<Car> GetAllCars()
    {
        return Cars;
    }

    public List<Car> GetCarsBasedOnPrice(double price)
    {
        var result = new List<Car>();

        foreach (Car car in Cars)
        {
            double currentPrice = price;

            if (car.Currency == "EUR")
            {
                currentPrice = price * 1.96;
            }

            if (car.Price < currentPrice)
            {
                result.Add(car);
            }
        }

        return result;
    }

    public double GetPriceOfAllCars()
    {
        double result = 0;

        foreach (Car car in Cars)
        {
            double price = car.Price;
            if (car.Currency == "EUR")
            {
                price *= 1.96;
            }

            result += price;
        }

        return result;
    }

    public List<Car> GetCarsByFilter()
    {
        var result = new List<Car>();

        foreach (Car car in Cars)
        {
            double price = car.Price;
            if (car.Currency == "EUR")
            {
                price *= 1.96;
            }

            if (car.Year > 2000 || price < 6000)
            {
                result.Add(car);
            }
        }

        return result;
    }

    public List<Car> GetCarsOrdered()
    {
        return Cars
            .OrderBy(car => car.Make)
            .ThenByDescending(car => car.Price)
            .ToList();
    }
}
