using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Student
    {
        public string StudentID { get; set; }
        public string UserID { get; set; }
        public string EntryLevel { get; set; }
        public User User { get; set; } // Lồng đối tượng User
    }
}
