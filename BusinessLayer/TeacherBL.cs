using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransferObject;

namespace BusinessLayer
{
    public class TeacherBL
    {
        private TeacherDL teacherDL = new TeacherDL();
        private UserBL userBL = new UserBL();

        public List<Teacher> GetAllTeachers()
        {
            try
            {
                return teacherDL.GetAllTeachers();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách giáo viên.", ex);
            }
        }

        public List<TeacherViewModel> GetTeacherViewModels()
        {
            try
            {
                return teacherDL.GetTeacherViewModels();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách giáo viên từ cơ sở dữ liệu.", ex);
            }
        }

        public Teacher GetTeacherByUserID(string userID)
        {
            try
            {
                return teacherDL.GetTeacherByUserID(userID);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy giáo viên theo UserID.", ex);
            }
        }

        public int AddTeacher(Teacher teacher)
        {
            try
            {
                return teacherDL.AddTeacher(teacher);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm giáo viên.", ex);
            }
        }

        public int UpdateTeacher(Teacher teacher)
        {
            try
            {
                return teacherDL.UpdateTeacher(teacher);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi cập nhật giáo viên.", ex);
            }
        }

        public int DeleteTeacher(string userId)
        {
            try
            {
                return teacherDL.DeleteTeacher(userId);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa giáo viên.", ex);
            }
        }

        public string GenerateTeacherID()
        {
            try
            {
                return teacherDL.GenerateTeacherID();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi sinh mã giáo viên tự động.", ex);
            }
        }
    }
}
