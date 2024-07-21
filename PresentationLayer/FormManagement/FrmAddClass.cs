using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer.FormManagement
{
    public partial class FrmAddClass : Form
    {
        private ClassBL classBL;
        private TeacherBL teacherBL;
        private CourseBL courseBL;

        private string classIdEdit = null;

        public FrmAddClass(string classId = null)
        {
            InitializeComponent();

            classBL = new ClassBL();
            teacherBL = new TeacherBL();
            courseBL = new CourseBL();

            classIdEdit = classId;
        }

        private void LoadCourses()
        {
            var courses = courseBL.GetAllCourse();
            cboCourse.DataSource = courses;
            cboCourse.DisplayMember = "CourseName";
            cboCourse.ValueMember = "CourseID";
        }

        private void LoadTeachers()
        {
            var allTeachers = teacherBL.GetAllTeachers();
            lstAvailableTeachers.DataSource = allTeachers;
            lstAvailableTeachers.DisplayMember = "FullName";
            lstAvailableTeachers.ValueMember = "TeacherID";
        }

        private void LoadClassInfoForEdit()
        {
            var cls = classBL.GetClassByID(classIdEdit);
            txtClassId.Text = cls.ClassID;
            cboCourse.SelectedValue = cls.CourseID;
            txtRoom.Text = cls.Room;
            nudMaxStudents.Value = cls.MaxStudents;
            dtpStartDate.Value = cls.StartDate;
            dtpEndDate.Value = cls.EndDate;

            var assigned = classBL.GetAssignedTeachers(classIdEdit);
            lstAssignedTeachers.DataSource = assigned;
            lstAssignedTeachers.DisplayMember = "FullName";
            lstAssignedTeachers.ValueMember = "TeacherID";
        }

        private void btnSaveClass_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtClassId.Text))
                {
                    MessageBox.Show("Mã lớp không được để trống.");
                    return;
                }

                if (dtpStartDate.Value >= dtpEndDate.Value)
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc.");
                    return;
                }

                var cls = new Class
                {
                    ClassID = txtClassId.Text,
                    CourseID = cboCourse.SelectedValue.ToString(),
                    Room = txtRoom.Text,
                    MaxStudents = (int)nudMaxStudents.Value,
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value
                };

                // Lấy danh sách giáo viên đã phân công
                var assignedTeachers = lstAssignedTeachers.DataSource as List<Teacher> ?? new List<Teacher>();
                var teacherIDs = assignedTeachers.Select(t => t.TeacherID).ToList();

                if (classIdEdit == null)
                {
                    classBL.AddClass(cls);
                    MessageBox.Show("Thêm lớp học thành công!");
                }
                else
                {
                    classBL.UpdateClass(cls);
                    MessageBox.Show("Cập nhật lớp học thành công!");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu lớp học: " + ex.Message);
            }
        }

        private void btnCancelClass_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddClass_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadTeachers();

            if (classIdEdit != null)
                LoadClassInfoForEdit();
            else
                txtClassId.Text = classBL.GenerateClassID();
        }

        private void btnAddTeacherToClass_Click(object sender, EventArgs e)
        {
            if (lstAvailableTeachers.SelectedItem != null)
            {
                var selected = (Teacher)lstAvailableTeachers.SelectedItem;
                var list = lstAssignedTeachers.DataSource as List<Teacher> ?? new List<Teacher>();
                if (!list.Any(t => t.TeacherID == selected.TeacherID))
                {
                    list.Add(selected);
                    lstAssignedTeachers.DataSource = null;
                    lstAssignedTeachers.DataSource = list;
                    lstAssignedTeachers.DisplayMember = "FullName";
                    lstAssignedTeachers.ValueMember = "TeacherID";
                }
            }
        }

        private void btnRemoveTeacherFromClass_Click(object sender, EventArgs e)
        {
            if (lstAssignedTeachers.SelectedItem != null)
            {
                var selected = (Teacher)lstAssignedTeachers.SelectedItem;
                var list = lstAssignedTeachers.DataSource as List<Teacher>;
                list.RemoveAll(t => t.TeacherID == selected.TeacherID);
                lstAssignedTeachers.DataSource = null;
                lstAssignedTeachers.DataSource = list;
                lstAssignedTeachers.DisplayMember = "FullName";
                lstAssignedTeachers.ValueMember = "TeacherID";
            }
        }
    }
}
