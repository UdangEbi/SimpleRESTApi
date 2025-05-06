using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class CourseEF : ICourse
    {
        private readonly ApplicationDbContext _context;
        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course: ", ex);
            }
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting course: {ex.Message}", ex);
            }
        }

        public ViewCourseWithCategory GetCourseById(int courseId)
        {
            var course = _context.Courses
                .Include(c => c.Category)
                .FirstOrDefault(c => c.CourseId == courseId);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            return new ViewCourseWithCategory
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Duration = course.Duration,
                CategoryId = course.CategoryId,
                CategoryName = course.Category?.CategoryName
            };
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
             return _context.Courses
                .Include(c => c.Category)
                .OrderByDescending(c => c.CourseName)
                .Select(c => new ViewCourseWithCategory
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category.CategoryName
                })
                .ToList();
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                existingCourse.CategoryId = course.CategoryId;
                _context.Courses.Update(existingCourse);
                _context.SaveChanges();
                return existingCourse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating course: {ex.Message}", ex);
            }
        }
    }
}