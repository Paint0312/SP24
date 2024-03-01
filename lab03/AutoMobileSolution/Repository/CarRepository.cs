using AutomobileLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobileLibrary.Repository
{
    public class CarRepository : ICarRepository
    {
        public void AddCar(Car car)
        {
            CarsDAO.Instance.AddCar(car);
        }

        public void DeleteCar(int id)
        {
            CarsDAO.Instance.DeleteCar(id);
        }

        public Car GetCarByID(int id)
        {
            return CarsDAO.Instance.GetCarByID(id);
        }

        public IEnumerable<Car> GetCars()
        {
            return CarsDAO.Instance.GetCars();
        }

        public void UpdateCar(Car car)
        {
            CarsDAO.Instance.UpdateCar(car);
        }
    }
}
