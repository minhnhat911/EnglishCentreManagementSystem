using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransferObject;

namespace BusinessLayer
{
    public class AccountBL
    {
        private AccountDL accountDL;
        private StudentBL studentBL;
        private TeacherBL teacherBL;
        private UserBL userBL;

        public AccountBL()
        {
            accountDL = new AccountDL();
            studentBL = new StudentBL();
            teacherBL = new TeacherBL();
            userBL = new UserBL();

        }


        public List<Account> GetAllAccounts()
        {
            try
            {
                return accountDL.GetAllAccounts();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách tài khoản từ cơ sở dữ liệu.", ex);
            }
        }
        public Account GetAccountByUserID(string userID)
        {
            try
            {
                return accountDL.GetAccountByUserID(userID);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy tài khoản theo UserID.", ex);
            }
        }


        public int AddAccount(Account acc)
        {
            int result = accountDL.AddAccount(acc);

            if (result > 0)
            {
                if (acc.Role.RoleID == 3)
                {
                    Student newStudent = new Student
                    {
                        StudentID = studentBL.GenerateStudentID(),
                        UserID = acc.UserID,
                        EntryLevel = "B1"
                    };
                    studentBL.AddStudent(newStudent);
                }
                else if (acc.Role.RoleID == 2)
                {
                    Teacher newTeacher = new Teacher
                    {
                        TeacherID = teacherBL.GenerateTeacherID(),
                        UserID = acc.UserID,
                        DegreeID = "DG01"
                    };
                    teacherBL.AddTeacher(newTeacher);
                }
            }

            return result;
        }


        public int DeleteAccount(string userId)
        {
            try
            {
                // Xóa học viên nếu có
                var student = studentBL.GetStudentByUserID(userId);
                if (student != null)
                {
                    studentBL.DeleteStudent(userId);
                }

                // Xóa giáo viên nếu có
                var teacher = teacherBL.GetTeacherByUserID(userId);
                if (teacher != null)
                {
                    teacherBL.DeleteTeacher(userId);
                }

                // Xóa account
                int accResult = accountDL.DeleteAccount(userId);

                // Xóa user
                userBL.DeleteUser(userId);

                return accResult;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa tài khoản và các dữ liệu liên quan.", ex);
            }
        }

        public int UpdateAccount(Account acc)
        {
            var currentAcc = GetAccountByUserID(acc.UserID); // lấy tài khoản hiện tại từ DB
            int result = accountDL.UpdateAccount(acc); // cập nhật Account

            if (result > 0)
            {
                int oldRoleID = currentAcc.Role.RoleID;
                int newRoleID = acc.Role.RoleID;

                if (oldRoleID != newRoleID)
                {
                    HandleRoleChange(acc.UserID, oldRoleID, newRoleID);
                }
            }

            return result;
        }
        private void HandleRoleChange(string userId, int oldRoleID, int newRoleID)
        {
            if (oldRoleID == 3 && newRoleID == 2)
            {
                // Chuyển từ Học viên sang Giáo viên
                studentBL.DeleteStudent(userId); // Xóa học viên nếu tồn tại
                Teacher newTeacher = new Teacher
                {
                    TeacherID = teacherBL.GenerateTeacherID(),
                    UserID = userId,
                    DegreeID = "MA" // Mặc định hoặc yêu cầu nhập
                };
                teacherBL.AddTeacher(newTeacher);
            }
            else if (oldRoleID == 2 && newRoleID == 3)
            {
                // Chuyển từ Giáo viên sang Học viên
                teacherBL.DeleteTeacher(userId); // Xóa giáo viên nếu tồn tại
                Student newStudent = new Student
                {
                    StudentID = studentBL.GenerateStudentID(),
                    UserID = userId,
                    EntryLevel = "B1" // Mặc định hoặc yêu cầu nhập
                };
                studentBL.AddStudent(newStudent);
            }
        }


    }
}
