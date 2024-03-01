using AutomobileLibrary.DataAccess;
using AutomobileLibrary.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomobileWebApp.Controllers
{
    public class CarsController : Controller
    {
        ICarRepository carRepository = null;
        public CarsController() => carRepository = new CarRepository();
        // GET: CarsController
        public ActionResult Index()
        {
            var CarList = carRepository.GetCars();
            return View(CarList);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cars = carRepository.GetCarByID(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // GET: CarsController/Create
        public ActionResult Create() => View();

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carRepository.AddCar(car);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(car);
            }
        }

        // GET: CarsController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cars = carRepository.GetCarByID(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                if (car != null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    carRepository.UpdateCar(car);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = "Error updating car";
                return View();
            }
        }

        // GET: CarsController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cars = carRepository.GetCarByID(id);
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                carRepository.DeleteCar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Error = "Error deleting car";
                return View();
            }
        }
    }
}
