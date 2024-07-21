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
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageTeacher : Form
    {
        private TeacherBL teacherBL;
        private AccountBL accountBL;
        private UserBL userBL;

        public FormManageTeacher()
        {
            InitializeComponent();
            teacherBL = new TeacherBL();
            accountBL = new AccountBL();
            userBL = new UserBL();
        }

        // Load giáo viên lên grid view
        private void LoadTeacherToGrid()
        {
            try
            {
                dgvTeacher.DataSource = null; // Xóa source cũ 
                dgvTeacher.DataSource = teacherBL.GetTeacherViewModels();
                dgvTeacher.Columns[4].Visible = false; //Ẩn cột không cần thiết
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Khi form load
        private void FormManageTeacher_Load(object sender, EventArgs e)
        {
            LoadTeacherToGrid(); // Hiển thị danh sách giáo viên khi load form
        }

        // Hiển thị chi tiết thông tin người dùng
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

        // Bắt sự kiện click vào từng dòng trong grid
        private void dgvTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                DataGridViewRow selectedRow = dgvTeacher.Rows[e.RowIndex];
                if (selectedRow == null || selectedRow.Cells["UserID"].Value == null)
                    return;

                string userId = selectedRow.Cells["UserID"].Value.ToString();

                // Xử lý nút Delete
                if (dgvTeacher.Columns[e.ColumnIndex].Name == "Delete")
                {
                    var result = MessageBox.Show("Bạn có chắc muốn xóa giáo viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            accountBL.DeleteAccount(userId); // Gọi hàm xóa tài khoản
                            RemoveDeleteColumnIfExists();
                            LoadTeacherToGrid();

                            MessageBox.Show("Xóa giáo viên và tài khoản thành công!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                        }
                    }
                    return;
                }

                // Xử lý nút Update
                if (dgvTeacher.Columns[e.ColumnIndex].Name == "Update")
                {
                    if (selectedRow.DataBoundItem is TeacherViewModel teacherViewModel)
                    {
                        FrmAddTeacher frm = new FrmAddTeacher(teacherViewModel); // Mở form sửa giáo viên
                        frm.ShowDialog();
                        RemoveUpdateColumnIfExists();
                        LoadTeacherToGrid();
                    }
                    return;
                }

                // Mặc định: Hiển thị chi tiết thông tin giáo viên
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

        // Xóa cột Delete nếu có
        private void RemoveDeleteColumnIfExists()
        {
            if (dgvTeacher.Columns.Contains("Delete"))
                dgvTeacher.Columns.Remove("Delete");
        }

        // Xóa cột Update nếu có
        private void RemoveUpdateColumnIfExists()
        {
            if (dgvTeacher.Columns.Contains("Update"))
                dgvTeacher.Columns.Remove("Update");
        }

        // Xử lý thêm giáo viên
        private void btnAddTeacher_Click_1(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();
            FrmAddTeacher frmAddTeacher = new FrmAddTeacher();
            frmAddTeacher.ShowDialog();
            LoadTeacherToGrid(); // Load lại danh sách sau khi thêm
        }

        // Xử lý thêm cột Delete khi cần
        private void btnDeleteTeacher_Click_1(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = "";
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 50;

            dgvTeacher.Columns.Add(btnDelete);
        }

        // Xử lý thêm cột Update khi cần
        private void btnUpdateTeacher_Click_1(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn();
            btnUpdate.Name = "Update";
            btnUpdate.HeaderText = "";
            btnUpdate.Text = "Sửa";
            btnUpdate.UseColumnTextForButtonValue = true;
            btnUpdate.Width = 50;

            dgvTeacher.Columns.Add(btnUpdate);
        }
    }
}
