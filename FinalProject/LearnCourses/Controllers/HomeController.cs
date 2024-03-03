using DataAccess.Repository.CourseRepo;
using LearnCourses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessObject.Models;

namespace LearnCourses
{
    public class HomeController : Controller
    {
        ICourseRepository courseRepository = new CourseRepository();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HomePage()
        {
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult Courses()
        {
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult CourseDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CourseDetails(int id)
        {
            Course course = courseRepository.GetCourseById(id);
            return View(course);
        }
    }
}
