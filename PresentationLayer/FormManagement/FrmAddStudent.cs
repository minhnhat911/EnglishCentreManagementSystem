using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FrmAddStudent : Form
    {
        private UserBL userBL;
        private AccountBL accountBL;
        private StudentBL studentBL;
        private ProficiencyLevelBL proficiencyLevelBL;

        private StudentViewModel EditStudent;

        private string generatedUserID;
        private string generatedStudentID; //Hiện UserID và StudentID trước khi thêm


        public FrmAddStudent(StudentViewModel editStudent = null)
        {
            InitializeComponent();
            userBL = new UserBL();
            accountBL = new AccountBL();
            studentBL = new StudentBL();
            this.EditStudent = editStudent;

            LoadRoles();
            proficiencyLevelBL = new ProficiencyLevelBL();
            LoadEntryLevels();

            if (EditStudent != null)
            {
                LoadStudentToForm(EditStudent);
            }
            else
            {
                generatedUserID = userBL.GenerateUserID(); //Tự động tạo userId
                generatedStudentID = studentBL.GenerateStudentID();
                txtUserId.Text = generatedUserID;
                txtStudentId.Text = generatedStudentID;
            }
        }

        private void LoadRoles()
        {
            cbbRole.Items.Add("Học viên");
            cbbRole.SelectedIndex = 0; 
            cbbRole.Enabled = false;
        }

        private void LoadEntryLevels()
        {
            cbbEntryLevel.Items.Clear();

            var levels = proficiencyLevelBL.GetAllLevels();
            foreach (var level in levels)
            {
                cbbEntryLevel.Items.Add(level.LevelName);
            }

            if (cbbEntryLevel.Items.Count > 0)
                cbbEntryLevel.SelectedIndex = 0;
        }

        private void LoadStudentToForm(StudentViewModel student)
        {
            txtStudentId.Text = student.StudentID;
            txtUserId.Text = student.UserID;

            // Load user data
            var user = userBL.GetUserByID(student.UserID);
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

            var accounts = accountBL.GetAllAccounts();
            //dùng LINQ
            var account = accounts.FirstOrDefault(a => a.UserID == student.UserID);

            //KHÔNG dùng LINQ
            /*Account account = null;
            foreach (var acc in accounts)
            {
                if (acc.UserID == student.UserID)
                {
                    account = acc;
                    break; // Dừng lại khi tìm thấy
                }
            }*/

            if (account != null)
            {
                txtUsername.Text = account.Username;
                txtPass.Text = account.PasswordHash;
                IsActive.Checked = account.IsActive;
            }


            cbbEntryLevel.SelectedItem = student.EntryLevel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string userID, studentID;

                if (EditStudent == null)
                {
                    userID = generatedUserID;
                    studentID = generatedStudentID;
                }
                else
                {
                    userID = EditStudent.UserID;
                    studentID = EditStudent.StudentID;
                }

                // Tạo đối tượng User
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

                // Tạo đối tượng Account
                Account acc = new Account
                {
                    UserID = userID,
                    Username = txtUsername.Text.Trim(),
                    PasswordHash = HashPassword(txtPass.Text.Trim()),
                    Role = new Role { RoleID = 3 }, // Role học viên
                    IsActive = IsActive.Checked
                };

                // Tạo đối tượng Student
                Student student = new Student
                {
                    StudentID = studentID,
                    UserID = userID,
                    User = user,
                    EntryLevel = cbbEntryLevel.SelectedItem != null ? cbbEntryLevel.SelectedItem.ToString() : "B1"
                };

                if (EditStudent == null) //Là tạo 
                {
                    // Kiểm tra trùng Username
                    List<Account> allAccounts = accountBL.GetAllAccounts();
                    bool isDuplicate = false;
                    foreach (Account existingAcc in allAccounts)
                    {
                        if (existingAcc.Username == acc.Username)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (isDuplicate)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Thêm mới
                    int userResult = userBL.AddUser(user);
                    int accResult = accountBL.AddAccount(acc);
                    // Không cần gọi AddStudent vì đã gọi trong AddAccount (nếu Role là học viên)

                    if (userResult > 0 && accResult > 0)
                    {
                        MessageBox.Show("Thêm học viên thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm học viên thất bại.");
                    }
                }
                else
                {
                    // Cập nhật
                    int userResult = userBL.UpdateUser(user);
                    int accResult = accountBL.UpdateAccount(acc);
                    int stuResult = studentBL.UpdateStudent(student);

                    if (userResult > 0 && accResult > 0 && stuResult > 0)
                    {
                        MessageBox.Show("Cập nhật học viên thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật học viên thất bại.");
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
            // Gợi ý: triển khai SHA256 hoặc thuật toán hash khác
            return password;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
