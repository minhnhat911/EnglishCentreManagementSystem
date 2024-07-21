using BusinessLayer;
using PresentationLayer.FormChild;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer
{
    public partial class FormMain : Form
    {
        private Account currentAccount = null; //ktra dang nhap 
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormChild.FormCourseMain formCourseMain = new FormChild.FormCourseMain();
            Addform(formCourseMain, pnlInfo);

            FormChild.FormLogin loginForm = new FormChild.FormLogin();
            // Đăng ký sự kiện
            loginForm.LoginSuccess += (acc) =>
            {
                currentAccount = acc;
                lblWelcome1.Text = $"Welcome, {acc.Role.RoleName}";
                pnlLeft.Controls.Clear();
                switch (acc.Role.RoleName)
                {
                    case "Admin":
                        Addform(new FormChild.FormMenuAdmin(Addform, pnlInfo), pnlLeft); //Truyeenf truc tiep
                        break;
                    case "Giáo viên":
                        Addform(new FormChild.FormMenuTeacher(), pnlLeft);
                        break;
                    case "Học viên":
                        Addform(new FormChild.FormMenuStudent(), pnlLeft);
                        break;
                }
                loginForm.Dispose();
            };
            Addform(loginForm, pnlLeft);
        }
        private void Addform(Form form, Panel panel)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel.Controls.Add(form);
            form.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (currentAccount == null)
            {
                MessageBox.Show("Bạn chưa đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                currentAccount = null; // Xóa trạng thái đăng nhập
                pnlLeft.Controls.Clear();
                pnlInfo.Controls.Clear();
                FormChild.FormCourseMain formCourseMain = new FormChild.FormCourseMain();
                Addform(formCourseMain, pnlInfo);
                lblWelcome1.Text = "";

                FormChild.FormLogin loginForm = new FormChild.FormLogin();
                loginForm.LoginSuccess += (acc) =>
                {
                    currentAccount = acc;
                    lblWelcome1.Text = $"Welcome, {acc.Role.RoleName}";
                    pnlLeft.Controls.Clear();
                    switch (acc.Role.RoleName)
                    {
                        case "Admin":
                            Addform(new FormChild.FormMenuAdmin(Addform, pnlInfo), pnlLeft);
                            break;
                        case "Giáo viên":
                            Addform(new FormChild.FormMenuTeacher(), pnlLeft);
                            break;
                        case "Học viên":
                            Addform(new FormChild.FormMenuStudent(), pnlLeft);
                            break;
                    }
                    loginForm.Dispose();
                };
                Addform(loginForm, pnlLeft);
            }
        }
    }
}
