using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class StudentTuitionFee
    {
        public int TuitionID { get; set; }
        public string StudentID { get; set; }
        public string StudentName { get; set; }      // Từ Users
        public string ClassID { get; set; }
        public string CourseName { get; set; }       // Từ Courses
        public decimal Fee { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

