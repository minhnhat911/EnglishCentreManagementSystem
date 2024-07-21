using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class StudentViewModel
    {
        public string StudentID { get; set; }
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EntryLevel { get; set; }

        // Thuộc tính để hiển thị ngày sinh đã định dạng
        public string DateOfBirthFormatted
        {
            get { return DateOfBirth.ToString("dd/MM/yyyy"); }
        }
    }
}
