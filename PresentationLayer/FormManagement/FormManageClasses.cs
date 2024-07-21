using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageClasses : Form
    {
        private ClassBL classBL;
        private ScheduleBL scheduleBL;

        public FormManageClasses()
        {
            InitializeComponent();
            classBL = new ClassBL();
            scheduleBL = new ScheduleBL();
        }

        //Load các lớp lên grid view
        private void LoadClassToGrid()
        {
            try
            {
                dgvClasses.DataSource = null;
                dgvClasses.DataSource = classBL.GetAllClasses();
                dgvClasses.Columns["CourseID"].Visible = false;
                dgvClasses.Columns[dgvClasses.Columns.Count - 1].Visible = false;
                RemoveUpdateColumnIfExists();
                RemoveDeleteColumnIfExists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message);
            }
        }

        private void FormManageClasses_Load(object sender, EventArgs e)
        {
            LoadClassToGrid(); // Khi form load thì hiển thị lớp học
        }


        private void RemoveDeleteColumnIfExists()
        {
            if (dgvClasses.Columns.Contains("Delete"))
                dgvClasses.Columns.Remove("Delete");
        }

        private void RemoveUpdateColumnIfExists()
        {
            if (dgvClasses.Columns.Contains("Update"))
                dgvClasses.Columns.Remove("Update");
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            FrmAddClass frmAddClass = new FrmAddClass(); // Form thêm lớp học
            frmAddClass.ShowDialog();
            LoadClassToGrid(); // Load lại sau khi thêm
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "";
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 50;

            dgvClasses.Columns.Add(btnDelete);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn();
            btnUpdate.Name = "Update";
            btnUpdate.HeaderText = "";
            btnUpdate.Text = "Sửa";
            btnUpdate.UseColumnTextForButtonValue = true;
            btnUpdate.Width = 50;

            dgvClasses.Columns.Add(btnUpdate);
        }

        private void dgvClasses_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (e.ColumnIndex >= dgvClasses.Columns.Count) return; // Kiểm tra chỉ số cột hợp lệ

            var clickedColumn = dgvClasses.Columns[e.ColumnIndex];
            if (clickedColumn == null) return;

            DataGridViewRow selectedRow = dgvClasses.Rows[e.RowIndex];
            string classId = selectedRow.Cells["ClassID"].Value.ToString();

            if (clickedColumn.Name == "Delete")
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        classBL.DeleteClass(classId);
                        RemoveDeleteColumnIfExists();

                        LoadClassToGrid();
                        MessageBox.Show("Xóa lớp thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message);
                    }
                }
            }
            else if (clickedColumn.Name == "Update")
            {
                FrmAddClass frm = new FrmAddClass(classId);
                frm.ShowDialog();
                RemoveUpdateColumnIfExists();
                LoadClassToGrid();
            }
        }
    }
}
