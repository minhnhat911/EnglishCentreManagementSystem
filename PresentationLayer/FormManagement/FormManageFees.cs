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
    public partial class FormManageFees : Form
    {
        private FeeBL feeBL;
        public FormManageFees()
        {
            InitializeComponent();
            feeBL = new FeeBL();
        }

        private void LoadFeesToGrid()
        {
            try
            {
                dgvFee.DataSource = null; //Xóa source cũ 
                dgvFee.DataSource = feeBL.GetAllStudentTuitionFees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormManageFees_Load(object sender, EventArgs e)
        {
            LoadFeesToGrid();
        }
    }
}
