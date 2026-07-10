using System;
using System.Linq;
using System.Collections.Generic;

namespace AutoService
{
    public class Program
    {
        public static Vehicle[] vehicleArray;
        public static bool isStarted = true;
        public static List<Vehicle> vehicles = new List<Vehicle>();

        static void Main(string[] args)
        {
            while (isStarted)
            {
                asd();
            }
        }

        private static void asd()
        {
            Console.Write("Write the number of vehicles to be inserted: ");
            int vehicleCount = Convert.ToInt32(Console.ReadLine());
            if (vehicleCount < 10 || vehicleCount > 500)
            {
                return;
            }
            
            Vehicle[] vehicleArr = new Vehicle[vehicleCount];

            for (int i = 0; i < vehicleCount; i++)
            {
                bool isEuro = false;

                int order = Convert.ToInt32(Console.ReadLine());
                string? make = Console.ReadLine();
                if (make.Length > 29)
                {
                    return;
                }

                int year = Convert.ToInt32(Console.ReadLine());
                double price = Convert.ToDouble(Console.ReadLine());
                string? currency = Console.ReadLine();
                if (currency == "EUR")
                {
                    isEuro = true;
                }

                var vehicle = new Vehicle(order, make, year, price, isEuro);

                vehicles.Add(vehicle);
                vehicleArr[i] = vehicle;
            }

            vehicleArray = vehicleArr;

            Console.WriteLine("All vehicles: ");
            PrintVehicles(vehicleArray);

            Console.WriteLine("Vehicles with price less than 300:");
            List<Vehicle> vehiclesByPrice = GetVehiclesListByPrice(300);
            PrintVehicles(vehiclesByPrice);

            Console.WriteLine("SUM:" + GetSumOfVehiclePrice());

            Console.WriteLine("Vehicles newer than 2000 and lower than 6000 bgn:");
            List<Vehicle> vehiclesWithFilter = GetVehiclesWithFilter();
            PrintVehicles(vehiclesWithFilter);

            Console.WriteLine("Ordered vehicles");
            PrintVehicles(GetOrderedVehicles());
        }

        public static void PrintVehicles(List<Vehicle> vehicleList)
        {
            for (int i = 0; i < vehicleList.Count; i++)
            {
                var vehicle = vehicleList[i];
                string currency = "BGN";
                if (vehicle.IsEuro)
                {
                    currency = "EUR";
                }

                Console.WriteLine($"{vehicle.Order}. Make: {vehicle.Make}, Year: {vehicle.Year}, Price: {vehicle.Price}{currency}");
            }
        }

        public static void PrintVehicles(Vehicle[] vehicleArr)
        {
            for (int i = 0; i < vehicleArr.Length; i++)
            {
                var vehicle = vehicleArr[i];
                string currency = "BGN";
                if (vehicle.IsEuro)
                {
                    currency = "EUR";
                }

                Console.WriteLine($"{vehicle.Order}. Make: {vehicle.Make}, Year: {vehicle.Year}, Price: {vehicle.Price}{currency}");
            }
        }

        public static List<Vehicle> GetVehiclesListByPrice(double price)
        {
            var result = new List<Vehicle>();
            foreach (var vehicle in vehicles)
            {
                if (!vehicle.IsEuro && vehicle.Price < price)
                {
                    result.Add(vehicle);
                    continue;
                }

                double vehiclePriceInEuro = vehicle.Price * 1.96;
                if (vehiclePriceInEuro < price)
                {
                    result.Add(vehicle);
                }
            }

            return result;
        }

        public static Vehicle[] GetVehiclesArrayByPrice(double price)
        {
            var result = new Vehicle[vehicleArray.Length];

            for (int i = 0; i < vehicleArray.Length; i++)
            {
                var vehicle = vehicleArray[i];
                if (!vehicle.IsEuro && vehicle.Price < price)
                {
                    result[i] = vehicle;
                    continue;
                }

                double vehiclePriceInEuro = vehicle.Price * 1.96;
                if (vehiclePriceInEuro < price)
                {
                    result[i] = vehicle;
                }
            }

            return result;
        }

        public static double GetSumOfVehiclePrice()
        {
            double sum = 0;

            for (int i = 0; i < vehicleArray.Length; i++)
            {
                sum += vehicleArray[i].Price;
            }

            return sum;
        }

        public static List<Vehicle> GetVehiclesWithFilter()
        {
            var result = new List<Vehicle>();

            foreach (var vehicle in vehicles)
            {
                if (vehicle.Year < 2000)
                {
                    continue;
                }

                double vehiclePrice = vehicle.Price;
                if (vehicle.IsEuro)
                {
                    vehiclePrice *= 1.96;
                }

                if (vehiclePrice < 6000) 
                {
                    result.Add(vehicle);
                }
            }

            return result;
        }

        public static List<Vehicle> GetOrderedVehicles()
        {
            return vehicles
                .OrderBy(vehicle => vehicle.Make)
                .ThenByDescending(vehicle => vehicle.Price)
                .ToList();
        }

        public class Vehicle 
        {
            public Vehicle(int order, string make, int year, double price, bool isEuro)
            {
                Order = order;
                Make = make;
                Year = year;
                Price = price;
                IsEuro = isEuro;
            }

            public int Order { get; set; }

            public string Make { get; set; }

            public int Year { get; set; }

            public double Price { get; set; }

            public bool IsEuro { get; set; } 
        }
    }
}