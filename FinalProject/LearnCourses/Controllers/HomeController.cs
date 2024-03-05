using DataAccess.Repository.CourseRepo;
using LearnCourses.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BusinessObject.Model;
using DataAccess.Repository.LessonRepo;
using DataAccess.Repository.UserRepo;
using DataAccess.Repository.OrderRepo;

namespace LearnCourses
{
    public class HomeController : Controller
    {
        IOrderRepository orderRepository = new OrderRepository();
        IUserRepository userRepository = new UserRepsitory();
        ILessonRepostory lessonRepository = new LessonRepository();
        ICourseRepository courseRepository = new CourseRepository();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public bool checkSession()
        {
            if (SessionExtensions.GetString(HttpContext.Session, "username") == null)
            {
                return true;
            }
            return false;
        }

        public IActionResult HomePage()
        {
            if(checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult Courses()
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            List<Course> listCourses = courseRepository.GetCourses().ToList();
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult CourseDetails(int id)
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            if (id == 0)
            {
                return RedirectToAction("Courses", "Home");
            }
            List<Lesson> listLessonByCourseID = lessonRepository.GetLessonByCourseId(id);
            List<Course> course = courseRepository.GetCourseById(id);
            ViewBag.Lessons = listLessonByCourseID;
            ViewBag.Course = course;
            return View();
        }
        public IActionResult Profile()
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            User user = userRepository.GetUserById(Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id")));
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(User user)
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            if (user != null)
            {
                int id = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));

                user.Enrollments = userRepository.GetUserById(id).Enrollments;
                user.Password = userRepository.GetUserById(id).Password;
                user.Balance = userRepository.GetUserById(id).Balance;
                user.UpdatedDate = DateTime.Now;

                userRepository.UpdateUser(user);
                TempData["Success"] = "Update successfully";
                return RedirectToAction("Profile", "Home");
            }
            TempData["Error"] = "Update failed";
            return RedirectToAction("Profile", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
        
        public IActionResult Cart()
        {
            if (checkSession())
            {
                return RedirectToAction("Login", "Users");
            }
            int id = Convert.ToInt32(SessionExtensions.GetString(HttpContext.Session, "id"));
            if (id == 0)
            {
                return RedirectToAction("Courses", "Home");
            }

            List<Course> listCourses = courseRepository.GetCoursesByUserID(id);
            ViewBag.Courses = listCourses;
            return View();
        }
        public IActionResult AddToCart(int id)
        {
            if (id != null)
            {
                var userIdString = SessionExtensions.GetString(HttpContext.Session, "id");
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    TempData["Error"] = "User ID not found in session or invalid";
                    return RedirectToAction("CourseDetails", "Home");
                }

                try
                {
                    orderRepository.AddOrder(new Order
                    {
                        CourseId = id,
                        UserId = userId,
                        CreatedDate = DateTime.Now
                    });
                    TempData["Success"] = "Add to cart successful";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Failed to add to cart: " + ex.Message;
                    // Log the inner exception for further investigation
                    if (ex.InnerException != null)
                    {
                        // Log the inner exception message
                        // Replace the following line with your logging mechanism
                        // logger.LogError("Inner Exception: " + ex.InnerException.Message);
                    }
                }

                return RedirectToAction("CourseDetails", "Home");
            }

            TempData["Error"] = "Add to cart failed";
            return RedirectToAction("CourseDetails", "Home");
        }
        public IActionResult Comment()
        {
            return View();
        }
    }
}
