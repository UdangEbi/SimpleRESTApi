using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class InstructorDal : IInstructor
    {
        private List<Instructor> _instructor = new List<Instructor>();

        public InstructorDal()
        {
            _instructor = new List<Instructor>
            {
                new Instructor { InstructorId = 1, InstructorName = "Alden Ray", InstructorEmail = "alden@gmail.com", InstructorPhone = "081234567890", InstructorAddress = "Jl. Merdeka No.10", InstructorCity = "Bandung"},
                new Instructor { InstructorId = 2, InstructorName = "Zyler Vaughn", InstructorEmail = "zyler@gmail.com", InstructorPhone = "082198765432", InstructorAddress = "Jl. Sudirman No. 25", InstructorCity = "Labuan Bajo"},
                new Instructor { InstructorId = 3, InstructorName = "Selena Kade", InstructorEmail = "selena@gmail.com", InstructorPhone = "085321098765", InstructorAddress = "Gg. Melati No. 7", InstructorCity = "Bogor"},
                new Instructor { InstructorId = 4, InstructorName = "Nixon Hale", InstructorEmail = "nixon@gmail.com", InstructorPhone = "087765432108", InstructorAddress = "Jl. Dipenogoro No. 52", InstructorCity = "Padang"},
                new Instructor { InstructorId = 5, InstructorName = "Elara Quinn", InstructorEmail = "elara@gmail.com", InstructorPhone = "089643210987", InstructorAddress = "Jl. Cendana No. 30", InstructorCity = "Semarang"}
            };
        }

        public IEnumerable<Instructor> GetInstructor()
        {
            return _instructor;
        }

        public Instructor GetInstructorById(int instructorId)
        {
            var instructor = _instructor.FirstOrDefault(c => c.InstructorId == instructorId);
            if (instructor == null)
            {
                throw new Exception ("Instructor not found");
            }
            return instructor;
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            _instructor.Add(instructor);
            return instructor;
        }

        public void DeleteInstructor(int instructorId)
        {
            var instructor = GetInstructorById(instructorId);
            if (instructor != null)
            {
                _instructor.Remove(instructor);
            }
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = GetInstructorById(instructor.InstructorId);
            if (existingInstructor != null)
            {
                existingInstructor.InstructorName = instructor.InstructorName;
                existingInstructor.InstructorEmail = instructor.InstructorEmail;
                existingInstructor.InstructorPhone = instructor.InstructorPhone;
                existingInstructor.InstructorAddress = instructor.InstructorAddress;
                existingInstructor.InstructorCity = instructor.InstructorCity;
            }
            return existingInstructor;
        }
    }
}