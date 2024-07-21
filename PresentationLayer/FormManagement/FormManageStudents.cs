using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageStudents : Form
    {
        private StudentBL studentBL; 
        private AccountBL accountBL;
        private UserBL userBL;
        public FormManageStudents()
        {
            InitializeComponent();
            studentBL = new StudentBL();
            accountBL = new AccountBL();
            userBL = new UserBL();
        }
        //Load Sinh viên lên grid view
        private void LoadStudentToGrid()
        {
            try
            {
                dgvStudent.DataSource = null; //Xóa source cũ 
                dgvStudent.DataSource = studentBL.GetStudentViewModels();
                dgvStudent.Columns[1].Visible = false;
                dgvStudent.Columns[dgvStudent.Columns.Count - 1].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormManageStudents_Load(object sender, EventArgs e)
        {   
            LoadStudentToGrid();
        }
        private void LoadUserDetail(string userId)
        {
            User user = userBL.GetUserByID(userId);
            if (user != null)
            {
                lblUserID.Text = user.UserID;
                lblName.Text = $"{user.LastName} {user.FirstName}";
                txtGender.Text = user.Gender;
                txtDateOfBirth.Text = user.DateOfBirth.ToString("dd/MM/yyyy");
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
                txtAddress.Text = user.Address;
                txtCreateAt.Text = user.CreatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "Chưa có dữ liệu";

                var account = accountBL.GetAllAccounts().FirstOrDefault(a => a.UserID == userId);
                txtRole.Text = account?.Role?.RoleName ?? "Không có vai trò";
            }
        }
        //Bắt sự kiện click vào từng dòng trên danh sách Sinh viên 
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];
                if (selectedRow == null || selectedRow.Cells["UserID"].Value == null)
                    return;

                string userId = selectedRow.Cells["UserID"].Value.ToString();

                // Xử lý nút Delete
                if (dgvStudent.Columns[e.ColumnIndex].Name == "Delete")
                {
                    var result = MessageBox.Show("Bạn có chắc muốn xóa học viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            accountBL.DeleteAccount(userId); // Gọi 1 hàm duy nhất
                            RemoveDeleteColumnIfExists();
                            LoadStudentToGrid();

                            MessageBox.Show("Xóa học viên và tài khoản thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                        }
                    }
                    return;
                }

                // Xử lý nút Update
                if (dgvStudent.Columns[e.ColumnIndex].Name == "Update")
                {
                    if (selectedRow.DataBoundItem is StudentViewModel student)
                    {
                        FrmAddStudent frm = new FrmAddStudent(student); // Phải có constructor nhận Student
                        frm.ShowDialog();
                        RemoveUpdateColumnIfExists();
                        LoadStudentToGrid();
                    }
                    return;
                }

                // Mặc định: hiển thị chi tiết
                pnlInfoUser.Visible = true;
                LoadUserDetail(userId);
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\nChi tiết lỗi: " + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void RemoveDeleteColumnIfExists()
        {
            if (dgvStudent.Columns.Contains("Delete"))
                dgvStudent.Columns.Remove("Delete");
        }

        private void RemoveUpdateColumnIfExists()
        {
            if (dgvStudent.Columns.Contains("Update"))
                dgvStudent.Columns.Remove("Update");
        }


        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            FormManagement.FrmAddStudent frmAddStudent = new FrmAddStudent();
            frmAddStudent.ShowDialog();
            LoadStudentToGrid(); // Load lại danh sách sau khi thêm
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "";
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 50;

            dgvStudent.Columns.Add(btnDelete);
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn();
            btnUpdate.Name = "Update";
            btnUpdate.HeaderText = "";
            btnUpdate.Text = "Sửa";
            btnUpdate.UseColumnTextForButtonValue = true;
            btnUpdate.Width = 50;

            dgvStudent.Columns.Add(btnUpdate);
        }


    }
}
