using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class CourseDTO
    {
         public int CourseId {get; set;}
        public string CourseName {get; set;} = null!;
        public string? CourseDescription {get; set;} = null!;
        public double Duration {get; set;}

        public CategoryDTO? Category { get; set; }

        public InstructorDTO? Instructor { get; set; }
    }
}