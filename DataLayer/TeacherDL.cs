using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class TeacherDL : DataProvider
    {
        // Lấy danh sách tất cả giáo viên
        public List<Teacher> GetAllTeachers()
        {
            string sql = @"
            SELECT t.TeacherID, t.UserID, t.DegreeID,
                   u.UserID, u.LastName, u.FirstName, u.Gender, u.DateOfBirth,
                   u.Email, u.Phone, u.Address, u.CreatedAt,
                   d.DegreeName
            FROM Teachers t
            JOIN Users u ON t.UserID = u.UserID
            JOIN Degrees d ON t.DegreeID = d.DegreeID";

            List<Teacher> teachers = new List<Teacher>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);

                while (reader.Read())
                {
                    User user = new User
                    {
                        UserID = reader["UserID"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        CreatedAt = reader["CreatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedAt"]) : (DateTime?)null
                    };

                    Degree degree = new Degree
                    {
                        DegreeID = reader["DegreeID"].ToString(),
                        DegreeName = reader["DegreeName"].ToString()
                    };

                    Teacher teacher = new Teacher
                    {
                        TeacherID = reader["TeacherID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        DegreeID = reader["DegreeID"].ToString(),
                        User = user,
                        Degree = degree
                    };

                    teachers.Add(teacher);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

            return teachers;
        }

        public List<TeacherViewModel> GetTeacherViewModels()
        {
            List<TeacherViewModel> teacherViewModels = new List<TeacherViewModel>();
            string proc = "sp_GetTeacherViewModels"; // Stored procedure lấy tất cả giáo viên

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(proc, CommandType.StoredProcedure);

                while (reader.Read())
                {
                    TeacherViewModel teacherViewModel = new TeacherViewModel
                    {
                        TeacherID = reader["TeacherID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        FullName = $"{reader["LastName"]} {reader["FirstName"]}",
                        Gender = reader["Gender"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        DegreeName = reader["DegreeName"].ToString()
                    };
                    teacherViewModels.Add(teacherViewModel);
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

            return teacherViewModels;
        }
        public Teacher GetTeacherByUserID(string userID)
        {
            string procName = "sp_GetTeacherByUserID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userID)
            };

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(procName, CommandType.StoredProcedure, parameters);
                if (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        TeacherID = reader["TeacherID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        DegreeID = reader["DegreeID"].ToString()
                    };
                    reader.Close();
                    return teacher;
                }

                reader.Close();
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy thông tin giáo viên theo UserID.", ex);
            }
            finally
            {
                Disconnect();
            }
        }


        // Thêm giáo viên mới
        public int AddTeacher(Teacher teacher)
        {
            string proc = "sp_AddTeacher";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TeacherID", teacher.TeacherID),
                new SqlParameter("@UserID", teacher.UserID),
                new SqlParameter("@DegreeID", teacher.DegreeID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(proc, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        // Cập nhật thông tin giáo viên
        public int UpdateTeacher(Teacher teacher)
        {
            string proc = "sp_UpdateTeacher";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", teacher.TeacherID),
                new SqlParameter("@DegreeID", teacher.Degree)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(proc, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        // Xóa giáo viên
        public int DeleteTeacher(string userID)
        {
            string proc = "sp_DeleteTeacher";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(proc, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        // Sinh mã giáo viên mới
        public string GenerateTeacherID()
        {
            string proc = "sp_GenerateTeacherID";

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(proc, CommandType.StoredProcedure);

                if (reader.Read())
                {
                    return reader["TeacherID"].ToString();
                }

                reader.Close();
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tạo mã giáo viên mới.", ex);
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
