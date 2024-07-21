using System;
using System.Collections.Generic;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class ClassBL
    {
        private ClassDL classDL;

        public ClassBL()
        {
            classDL = new ClassDL();
        }

        // Lấy danh sách tất cả các lớp
        public List<Class> GetAllClasses()
        {
            try
            {
                return classDL.GetAllClasses();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách lớp học.", ex);
            }
        }

        // Lấy thông tin lớp học theo ClassID
        public Class GetClassByID(string classID)
        {
            try
            {
                return classDL.GetClassByID(classID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin lớp học với ID: {classID}", ex);
            }
        }

        // Lấy danh sách giáo viên giảng dạy lớp học theo ClassID
        public List<Teacher> GetAssignedTeachers(string classID)
        {
            try
            {
                return classDL.GetAssignedTeachers(classID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách giáo viên giảng dạy lớp học với ID: {classID}", ex);
            }
        }

        // Thêm một lớp học mới
        public int AddClass(Class cls)
        {
            try
            {
                // Kiểm tra các điều kiện hoặc logic nghiệp vụ trước khi thêm
                return classDL.AddClass(cls);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm lớp học mới.", ex);
            }
        }

        // Cập nhật thông tin lớp học
        public int UpdateClass(Class cls)
        {
            try
            {
                // Kiểm tra các điều kiện hoặc logic nghiệp vụ trước khi cập nhật
                return classDL.UpdateClass(cls);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật lớp học.", ex);
            }
        }

        // Xóa lớp học
        public int DeleteClass(string classID)
        {
            try
            {
                // Kiểm tra các điều kiện hoặc logic nghiệp vụ trước khi xóa
                return classDL.DeleteClass(classID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa lớp học với ID: {classID}", ex);
            }
        }

        // Sinh mã lớp học tự động
        public string GenerateClassID()
        {
            try
            {
                return classDL.GenerateClassID();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sinh mã lớp học.", ex);
            }
        }
    }
}
