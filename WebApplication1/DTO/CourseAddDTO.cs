using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class CourseAddDTO
    {
        public string CourseName {get; set;} = null!;
        public string? CourseDescription {get; set;} = null!;
        public double Duration {get; set;}
        public int CategoryId {get; set; }
        public int InstructorId {get; set; }
        public CategoryDTO? Category { get; set; }

        public InstructorDTO? Instructor {get; set;}
    }
}