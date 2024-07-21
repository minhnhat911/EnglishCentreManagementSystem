// FormLogin.cs
using BusinessLayer;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TransferObject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PresentationLayer.FormChild
{
    public partial class FormLogin : Form
    {
        private LoginBL loginBL;
        public event Action<Account> LoginSuccess; // Khai báo sự kiện

        public FormLogin()
        {
            InitializeComponent();
            loginBL = new LoginBL();  
            txtPassword.PasswordChar = '*';
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            Account acc = loginBL.Login(username, password);

            if (acc != null)
            {
                LoginSuccess?.Invoke(acc);
                MessageBox.Show("Đăng nhập thành công. Vai trò: " + acc.Role.RoleName);               
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {           
            txtPassword.PasswordChar = '\0';
        }
    }
}
