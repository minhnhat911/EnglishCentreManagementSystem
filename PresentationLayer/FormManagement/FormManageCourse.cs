using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageCourse : Form
    {
        private CourseBL courseBL;
        public FormManageCourse()
        {
            InitializeComponent();
            courseBL = new CourseBL();

        }
        private void LoadAccountsToGrid()
        {
            try
            {
                dgvCourse.DataSource = null; //Xóa source cũ 
                dgvCourse.DataSource = courseBL.GetAllCourse();
                dgvCourse.Columns[dgvCourse.Columns.Count - 1].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormManageCourse_Load(object sender, EventArgs e)
        {
            LoadAccountsToGrid();
        }
    }
}
