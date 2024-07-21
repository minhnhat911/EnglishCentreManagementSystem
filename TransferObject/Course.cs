using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Course
    {
        public string CourseID { get; set; }        // Mã khóa học
        public string CourseName { get; set; }      // Tên khóa học
        public string Description { get; set; }     // Mô tả
        public int Duration { get; set; }           // Thời lượng (giờ hoặc buổi học)
        public decimal Fee { get; set; }            // Học phí
    }

}
