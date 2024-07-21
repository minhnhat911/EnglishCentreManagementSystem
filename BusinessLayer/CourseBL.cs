using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataLayer;
using TransferObject;

namespace BusinessLayer
{
    public class CourseBL
    {
        private CourseDL courseDL = new CourseDL();
        public List<Course> GetAllCourse()
        {
            try
            {
                return courseDL.GetAllCourses();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách giáo viên.", ex);
            }
        }
        public int AddCourse(Course course)
        {
            try
            {
                return courseDL.AddCourse(course);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm giáo viên.", ex);
            }
        }

        public int UpdateTeacher(Course course)
        {
            try
            {
                return courseDL.UpdateCourse(course);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi cập nhật giáo viên.", ex);
            }
        }

        public int DeleteCourse(string userId)
        {
            try
            {
                return courseDL.DeleteCourse(userId);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa giáo viên.", ex);
            }
        }
    }
}
