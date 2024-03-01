using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileLibrary.DataAccess
{
    public class CarsDAO
    {
        private static CarsDAO instance = null;
        private static readonly object instacelock = new object();
        
        public static CarsDAO Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new CarsDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>();
            try
            {
                using var context = new CarDBContext();
                cars = context.Cars.ToList();
            } catch (Exception ex)
            {
                throw new Exception("Error getting cars", ex);
            }
            return cars;
        }   
        public Car GetCarByID(int id)
        {
            Car car = null;
            try
            {
                using var context = new CarDBContext();
                car = context.Cars.SingleOrDefault(c => c.CarId == id);
            } catch (Exception ex)
            {
                throw new Exception("Error getting car", ex);
            }
            return car;
        }
        public void AddCar(Car car)
        {
            try
            {
                using var context = new CarDBContext();
                context.Cars.Add(car);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error adding car", ex);
            }
        }
        public void UpdateCar(Car car)
        {
            try
            {
                using var context = new CarDBContext();
                context.Cars.Update(car);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error updating car", ex);
            }
        }
        public void DeleteCar(int id)
        {
            try
            {
                using var context = new CarDBContext();
                var car = context.Cars.SingleOrDefault(c => c.CarId == id);
                context.Cars.Remove(car);
                context.SaveChanges();
            } catch (Exception ex)
            {
                throw new Exception("Error deleting car", ex);
            }
        }
    }
}
