using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        public void AddCourse(Course course)
        {
            CourseDAO.Instance.AddCourse(course);
        }

        public void DeleteCourse(Course course)
        {
            CourseDAO.Instance.DeleteCourse(course);
        }

        public Course GetCourseById(int id)
        {
            return CourseDAO.Instance.GetCourseById(id);
        }

        public IEnumerable<Course> GetCourses()
        {
            return CourseDAO.Instance.GetCourses();
        }

        public void UpdateCourse(Course course)
        {
            CourseDAO.Instance.UpdateCourse(course);
        }
    }
}
