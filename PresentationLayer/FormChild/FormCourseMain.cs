using BusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.FormChild
{
    public partial class FormCourseMain : Form
    {
        private CourseBL courseBL;
        public FormCourseMain()
        {
            InitializeComponent();
            courseBL = new CourseBL();

        }

        private void dgvCourseMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FormCourseMain_Load(object sender, EventArgs e)
        {
            dgvCourseMain.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold); // Chỉnh cỡ chữ cho tiêu đề cột
            dgvCourseMain.ReadOnly = true;
            dgvCourseMain.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10); 
            var courseList = courseBL.GetAllCourse();

            // Chỉ tạo bảng gồm CourseName và Description
            DataTable table = new DataTable();
            table.Columns.Add(">>", typeof(string));
            table.Columns.Add("Course Name", typeof(string));
            table.Columns.Add("Description", typeof(string));

            foreach (var course in courseList)
            {
                table.Rows.Add(">>", course.CourseName, course.Description);
            }

            dgvCourseMain.DataSource = table;

            // Tùy chỉnh cột
            dgvCourseMain.Columns[0].Width = 30; // Cột >>
            dgvCourseMain.Columns[1].Width = 200;
            dgvCourseMain.Columns[2].Width = 300;
        }
    }
}
