using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FrmAddAccount : Form
    {
        private UserBL userBL;
        private AccountBL accountBL; // Thêm AccountBL để xử lý tài khoản
        private Account EditAccount;

        private string generatedUserID;
        public FrmAddAccount(Account editAccount = null)
        {
            InitializeComponent();
            userBL = new UserBL();
            accountBL = new AccountBL();
            this.EditAccount = editAccount;

            LoadRoles();

            if (EditAccount != null)
            {
                LoadAccountToForm(EditAccount);
            }
            else
            {
                generatedUserID = userBL.GenerateUserID();
                txtUserId.Text = generatedUserID;
            }
        }
        private void LoadRoles()
        {
            try
            {
                List<Role> roles = userBL.GetAllRoles();
                cbbRole.DataSource = roles;
                cbbRole.DisplayMember = "RoleName";
                cbbRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách Role: {ex.Message}");
            }
        }
        private void LoadAccountToForm(Account acc)
        {
            txtUserId.Text = acc.UserID;
            txtUsername.Text = acc.Username;
            txtPass.Text = acc.PasswordHash;
            cbbRole.SelectedValue = acc.RoleID;
            IsActive.Checked = acc.IsActive;

            var user = userBL.GetUserByID(acc.UserID);
            if (user != null)
            {
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                cboGender.Text = user.Gender;
                dtpDateOfBirth.Value = user.DateOfBirth;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
                txtAddress.Text = user.Address;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string userID;

                if (EditAccount == null)
                {
                    userID = generatedUserID;
                }
                else
                {
                    userID = EditAccount.UserID;
                }

                User user = new User
                {
                    UserID = userID,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Gender = cboGender.Text,
                    DateOfBirth = dtpDateOfBirth.Value,
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    CreatedAt = DateTime.Now
                };

                Account acc = new Account
                {
                    UserID = userID,
                    Username = txtUsername.Text.Trim(),
                    PasswordHash = HashPassword(txtPass.Text.Trim()),
                    Role = new Role { RoleID = Convert.ToInt32(cbbRole.SelectedValue) },
                    IsActive = IsActive.Checked
                };

                if (EditAccount == null)
                {
                    // Kiểm tra trùng Username
                    List<Account> allAccounts = accountBL.GetAllAccounts();
                    bool isDuplicate = false;
                    foreach (Account a in allAccounts)
                    {
                        if (a.Username == acc.Username)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (isDuplicate)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại.");
                        return;
                    }

                    int userResult = userBL.AddUser(user);
                    if (userResult <= 0)
                    {
                        MessageBox.Show("Thêm người dùng thất bại.");
                        return;
                    }

                    int accResult = accountBL.AddAccount(acc);
                    if (accResult > 0)
                        MessageBox.Show("Thêm tài khoản thành công.");
                    else
                        MessageBox.Show("Thêm tài khoản thất bại.");
                }
                else
                {
                    int userResult = userBL.UpdateUser(user);
                    int accResult = accountBL.UpdateAccount(acc);

                    if (userResult > 0 && accResult > 0)
                        MessageBox.Show("Cập nhật tài khoản thành công.");
                    else
                        MessageBox.Show("Cập nhật tài khoản thất bại.");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Hàm hash mật khẩu (Bạn cần tự triển khai)
        private string HashPassword(string password)
        {
            // Cần triển khai hàm hash mật khẩu ở đây
            return password; // Đây là ví dụ, bạn cần sử dụng thuật toán hash như SHA256 hoặc Bcrypt.
        }

        private void AddAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
