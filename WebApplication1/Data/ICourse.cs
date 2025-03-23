using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface ICourse
    {
        IEnumerable<ViewCourseWithCategory> GetCourses();
        ViewCourseWithCategory GetCourseById(int courseId);
        Course AddCourse(Course course);
        Course UpdateCourse(Course course);
        void DeleteCourse(int courseId);
    }
}