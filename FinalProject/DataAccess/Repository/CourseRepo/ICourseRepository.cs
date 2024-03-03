using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.CourseRepo
{
    public interface ICourseRepository
    {
        public Course GetCourseById(int id);
        public IEnumerable<Course> GetCourses();
        public void AddCourse(Course course);
        public void UpdateCourse(Course course);
        public void DeleteCourse(Course course);
    }
}
