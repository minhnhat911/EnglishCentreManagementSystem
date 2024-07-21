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
    public partial class FrmAddResult : Form
    {
        private StudentBL studentBL;
        private PlacementTestBL placementTestBL;
        private PlacementTest currentTest;
        private string generatedTestID;

        public FrmAddResult(PlacementTest test = null)
        {
            InitializeComponent();
            studentBL = new StudentBL();
            placementTestBL = new PlacementTestBL(); // Khởi tạo đối tượng PlacementTestBL
            currentTest = test; // Lưu lại đối tượng test (nếu có)
        }

        private void FrmAddResult_Load(object sender, EventArgs e)
        {
            LoadStudentIDsToComboBox();

            // Điền thông tin của PlacementTest vào các điều khiển nếu currentTest không null
            if (currentTest != null)
            {
                cbbStudentID.SelectedValue = currentTest.StudentID; // Chọn học viên tương ứng
                txtTestID.Text = currentTest.TestID;
                txtScore.Text = currentTest.Score.ToString(); // Nếu Score là kiểu số
                txtLevel.Text = currentTest.LevelName;

                // Nếu có trường DateTest (Ngày kiểm tra), bạn có thể điền vào như sau:
                dateTest.Value = currentTest.TestDate; // Giả sử TestDate là kiểu DateTime
            }
            else
            {
                generatedTestID = placementTestBL.GenerateTestID();
                txtTestID.Text = generatedTestID; // Tạo TestID mới
            }
        }

        private void LoadStudentIDsToComboBox()
        {
            try
            {
                List<Student> students = studentBL.GetAllStudents();

                // Binding vào ComboBox
                cbbStudentID.DataSource = students;
                cbbStudentID.DisplayMember = "StudentID"; // Hoặc có thể sử dụng "FullName" nếu bạn muốn hiển thị tên
                cbbStudentID.ValueMember = "StudentID";
                cbbStudentID.SelectedIndex = -1; // Không chọn mặc định
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học viên: " + ex.Message);
            }
        }

        // Xử lý sự kiện lưu kết quả
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các điều kiện nhập liệu
                if (cbbStudentID.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn học viên!");
                    return;
                }

                if (string.IsNullOrEmpty(txtScore.Text) || !float.TryParse(txtScore.Text, out float score))
                {
                    MessageBox.Show("Điểm thi không hợp lệ!");
                    return;
                }

                string studentID = cbbStudentID.SelectedValue.ToString();
                string testID = txtTestID.Text; // Sử dụng TestID đã được tạo tự động
                string level = txtLevel.Text;
                DateTime testDate = dateTest.Value;

                // Kiểm tra nếu currentTest là null thì là thêm mới, nếu không là cập nhật
                if (currentTest == null)
                {
                    // Tạo mới đối tượng PlacementTest
                    PlacementTest newTest = new PlacementTest
                    {
                        StudentID = studentID,
                        TestID = testID,
                        Score = score,
                        LevelName = level,
                        TestDate = testDate
                    };

                    // Gọi hàm AddPlacementTest để thêm kết quả mới
                    placementTestBL.AddPlacementTest(newTest);
                    MessageBox.Show("Thêm mới kết quả thành công!");
                }
                else
                {
                    // Cập nhật PlacementTest hiện tại
                    currentTest.StudentID = studentID;
                    currentTest.TestID = testID;
                    currentTest.Score = score;
                    currentTest.LevelName = level;
                    currentTest.TestDate = testDate;

                    // Gọi hàm UpdatePlacementTest để cập nhật kết quả
                    placementTestBL.UpdatePlacementTest(currentTest);
                    MessageBox.Show("Cập nhật kết quả thành công!");
                }

                // Đóng form sau khi lưu thành công
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
            }
        }

        // Sự kiện đóng form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
