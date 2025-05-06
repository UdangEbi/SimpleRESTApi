using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Category
    {
        public int CategoryId {get; set; }
        
        public string CategoryName {get; set; } = null!;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}