using BusinessLayer;
using DataLayer;
using System;
using System.Linq;
using System.Windows.Forms;
using TransferObject;
using System.Collections.Generic;

namespace PresentationLayer.FormManagement
{
    public partial class FrmAddTeacher : Form
    {
        private UserBL userBL;
        private AccountBL accountBL;
        private TeacherBL teacherBL;
        private DegreeBL degreeBL;
        private TeacherViewModel EditTeacher;

        private string generatedUserID;
        private string generatedTeacherID;

        public FrmAddTeacher(TeacherViewModel editTeacher = null)
        {
            InitializeComponent();
            userBL = new UserBL();
            accountBL = new AccountBL();
            teacherBL = new TeacherBL();
            degreeBL = new DegreeBL();
            this.EditTeacher = editTeacher;

            LoadRoles();
            LoadDegreesToComboBox();

            if (EditTeacher != null)
            {
                LoadTeacherToForm(EditTeacher);
            }
            else
            {
                generatedUserID = userBL.GenerateUserID();
                generatedTeacherID = teacherBL.GenerateTeacherID();
                txtUserId.Text = generatedUserID;
                txtTeacherId.Text = generatedTeacherID;
            }
        }

        private void LoadRoles()
        {
            cbbRole.Items.Add("Giáo viên");
            cbbRole.SelectedIndex = 0;
            cbbRole.Enabled = false;
        }

        private void LoadDegreesToComboBox()
        {
            var degrees = degreeBL.GetAllDegrees();
            cbbDegree.DataSource = degrees;
            cbbDegree.DisplayMember = "DegreeName";
            cbbDegree.ValueMember = "DegreeID";
        }

        private void LoadTeacherToForm(TeacherViewModel teacher)
        {
            txtTeacherId.Text = teacher.TeacherID;
            txtUserId.Text = teacher.UserID;

            var user = userBL.GetUserByID(teacher.UserID);
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

            var account = accountBL.GetAllAccounts().FirstOrDefault(a => a.UserID == teacher.UserID);
            if (account != null)
            {
                txtUsername.Text = account.Username;
                txtPass.Text = account.PasswordHash;
                IsActive.Checked = account.IsActive;
            }

            cbbDegree.SelectedValue = teacher.DegreeName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string userID = EditTeacher == null ? generatedUserID : EditTeacher.UserID;
                string teacherID = EditTeacher == null ? generatedTeacherID : EditTeacher.TeacherID;
                string degreeID = cbbDegree.SelectedValue.ToString();

                // Tạo User
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

                // Tạo Account
                Account account = new Account
                {
                    UserID = userID,
                    Username = txtUsername.Text.Trim(),
                    PasswordHash = HashPassword(txtPass.Text.Trim()),
                    Role = new Role { RoleID = 2 }, // Role giáo viên
                    IsActive = IsActive.Checked
                };

                // Tạo Teacher
                Teacher teacher = new Teacher
                {
                    TeacherID = teacherID,
                    UserID = userID,
                    DegreeID = degreeID,
                    User = user
                };

                // Kiểm tra trùng username
                bool isDuplicate = accountBL.GetAllAccounts()
                                            .Any(a => a.Username == account.Username && a.UserID != userID);
                if (isDuplicate)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (EditTeacher == null)
                {
                    int userResult = userBL.AddUser(user);
                    int accResult = accountBL.AddAccount(account);

                    if (userResult > 0 && accResult > 0)
                    {
                        int teacherResult = teacherBL.AddTeacher(teacher);
                        if (teacherResult > 0)
                        {
                            MessageBox.Show("Thêm giáo viên thành công!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm giáo viên vào bảng Teachers thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thêm người dùng hoặc tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int userResult = userBL.UpdateUser(user);
                    int accResult = accountBL.UpdateAccount(account);
                    int teacherResult = teacherBL.UpdateTeacher(teacher);

                    if (userResult > 0 && accResult > 0 && teacherResult > 0)
                    {
                        MessageBox.Show("Cập nhật giáo viên thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật giáo viên thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Lỗi: " + ex.Message;
                if (ex.InnerException != null)
                    msg += "\nChi tiết: " + ex.InnerException.Message;

                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            // TODO: Mã hóa thực sự trong môi trường production
            return password;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
