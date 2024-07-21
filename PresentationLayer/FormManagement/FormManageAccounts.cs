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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FormManageAccounts : Form
    {
        private AccountBL accountBL;
        private UserBL userBL;
        public FormManageAccounts()
        {
            InitializeComponent();
            accountBL = new AccountBL();
            userBL = new UserBL();
        }
      

        private void LoadAccountsToGrid()
        {
            try
            {
                dgvAccounts.DataSource = null; //Xóa source cũ 
                dgvAccounts.DataSource = accountBL.GetAllAccounts();
                dgvAccounts.Columns[dgvAccounts.Columns.Count - 1].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormManageUsers_Load(object sender, EventArgs e)
        {

            LoadAccountsToGrid();
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

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 1. Kiểm tra hợp lệ
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                // 2. Lấy dòng và kiểm tra tồn tại
                DataGridViewRow selectedRow = dgvAccounts.Rows[e.RowIndex];
                if (selectedRow == null || selectedRow.Cells["UserID"].Value == null)
                    return;

                string userId = selectedRow.Cells["UserID"].Value.ToString();

                // 3. Xử lý nút Delete
                if (dgvAccounts.Columns[e.ColumnIndex].Name == "Delete")
                {
                    var result = MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        accountBL.DeleteAccount(userId);
                        RemoveDeleteColumnIfExists();
                        LoadAccountsToGrid();
                    }
                    return; // Dừng luôn sau khi xử lý Delete
                }

                // 4. Xử lý nút Update
                if (dgvAccounts.Columns[e.ColumnIndex].Name == "Update")
                {
                    if (selectedRow.DataBoundItem is Account acc)
                    {
                        FrmAddAccount frm = new FrmAddAccount(acc); // Mở form sửa
                        frm.ShowDialog();
                        RemoveUpdateColumnIfExists();
                        LoadAccountsToGrid();
                    }
                    return; // Dừng sau khi xử lý Update
                }

                // 5. Mặc định: hiển thị chi tiết user
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
            if (dgvAccounts.Columns.Contains("Delete"))
                dgvAccounts.Columns.Remove("Delete");
        }    
        private void RemoveUpdateColumnIfExists()
        {
            if (dgvAccounts.Columns.Contains("Update"))
                dgvAccounts.Columns.Remove("Update");
        }       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();
            FormManagement.FrmAddAccount formAddAccount = new FrmAddAccount();
            formAddAccount.ShowDialog();
            LoadAccountsToGrid();//Load lại danh sách 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();
            // Tạo cột nút Delete mới
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "Delete";
            btnDelete.HeaderText = ""; // Không hiển thị tiêu đề
            btnDelete.Text = "X";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 50;

            // Thêm vào cuối danh sách cột
            dgvAccounts.Columns.Add(btnDelete);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            RemoveDeleteColumnIfExists();
            RemoveUpdateColumnIfExists();

            // Tạo cột nút Update mới
            DataGridViewButtonColumn btnUpdate = new DataGridViewButtonColumn();
            btnUpdate.Name = "Update";
            btnUpdate.HeaderText = ""; // Không hiển thị tiêu đề
            btnUpdate.Text = "Sửa";
            btnUpdate.UseColumnTextForButtonValue = true;
            btnUpdate.Width = 50;

            // Thêm vào cuối danh sách cột
            dgvAccounts.Columns.Add(btnUpdate);
        }
    }
 }
