using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IInstructor
    {
        IEnumerable<Instructor> GetInstructor();
        Instructor GetInstructorById(int instructorId);
        Instructor AddInstructor(Instructor instructor);
        Instructor UpdateInstructor(Instructor instructor);
        void DeleteInstructor(int instructorId);
    }
}