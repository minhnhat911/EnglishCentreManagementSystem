using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransferObject;

namespace BusinessLayer
{
    public class StudentBL
    {
        private StudentDL studentDL = new StudentDL();
        private UserBL userBL = new UserBL();
        public List<Student> GetAllStudents()
        {
            try
            {
                return studentDL.GetAllStudents();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học viên từ cơ sở dữ liệu.", ex);
            }
        }
        public List<StudentViewModel> GetStudentViewModels()
        {
            try
            {
                return studentDL.GetStudentViewModels();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học viên từ cơ sở dữ liệu.", ex);
            }
        }

        public Student GetStudentByUserID(string userID)
        {
            try
            {
                return studentDL.GetStudentByUserID(userID);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy học viên theo UserID.", ex);
            }
        }

        public int AddStudent(Student student)
        {
            try
            {
                return studentDL.AddStudent(student);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm học viên vào cơ sở dữ liệu.", ex);
            }
        }

        public int UpdateStudent(Student student)
        {
            try
            {
                return studentDL.UpdateStudent(student);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin học viên.", ex);
            }
        }

        public int DeleteStudent(string userId)
        {
            try
            {
                // Chỉ xóa học viên, không gọi tới Account hoặc User để tránh vòng lặp
                return studentDL.DeleteStudent(userId);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa học viên.", ex);
            }
        }

        public string GenerateStudentID()
        {
            try
            {
                return studentDL.GenerateStudentID();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tạo mã học viên mới.", ex);
            }
        }
    }
}
