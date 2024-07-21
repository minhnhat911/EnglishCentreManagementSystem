using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Teacher
    {
        public string TeacherID { get; set; }
        public string UserID { get; set; }
        public string DegreeID { get; set; }

        public User User { get; set; }           // Liên kết đến User
        public Degree Degree { get; set; }       // Liên kết đến Degree
    }
}
