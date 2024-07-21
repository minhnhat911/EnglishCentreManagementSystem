using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class StudentDL:DataProvider
    {
        public List<Student> GetAllStudents()
        {
            string sql = @"
                        SELECT s.StudentID, s.EntryLevel,
                               u.LastName, u.FirstName, u.Gender, u.DateOfBirth
                        FROM Students s
                        JOIN Users u ON s.UserID = u.UserID";

            List<Student> students = new List<Student>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    Student student = new Student
                    {
                        StudentID = reader["StudentID"].ToString(),
                        EntryLevel = reader["EntryLevel"].ToString(),
                        User = new User
                        {
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"]
                        }
                    };

                    students.Add(student);
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

            return students;
        }
        public List<StudentViewModel> GetStudentViewModels()
        {
            string sql = @"
                            SELECT s.StudentID, s.EntryLevel, s.UserID,
                                   u.LastName, u.FirstName, u.Gender, u.DateOfBirth
                            FROM Students s
                            JOIN Users u ON s.UserID = u.UserID";

            List<StudentViewModel> result = new List<StudentViewModel>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    var viewModel = new StudentViewModel
                    {
                        StudentID = reader["StudentID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        FullName = $"{reader["LastName"]} {reader["FirstName"]}",
                        Gender = reader["Gender"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        EntryLevel = reader["EntryLevel"].ToString()
                    };

                    result.Add(viewModel);
                }
                reader.Close();
            }
            finally
            {
                Disconnect();
            }

            return result;
        }
        public Student GetStudentByUserID(string userID)
        {
            string procName = "sp_GetStudentByUserID";
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
                    Student student = new Student
                    {
                        StudentID = reader["StudentID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        EntryLevel = reader["EntryLevel"].ToString()
                    };
                    reader.Close();
                    return student;
                }

                reader.Close();
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy thông tin học viên theo UserID.", ex);
            }
            finally
            {
                Disconnect();
            }
        }


        public int AddStudent(Student student)
        {
            string sql = "INSERT INTO Students (StudentID, UserID, EntryLevel) " +
                "VALUES (@StudentID, @UserID, @EntryLevel)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
            new SqlParameter("@StudentID", student.StudentID),
            new SqlParameter("@UserID", student.UserID), // Lấy từ đối tượng User
            new SqlParameter("@EntryLevel", student.EntryLevel)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(sql, CommandType.Text, parameters); // Trả về số dòng ảnh hưởng
            }
            finally
            {
                Disconnect();
            }
        }
        
        // Cập nhật thông tin Student
        public int UpdateStudent(Student student)
        {
            string sql = @"UPDATE Students
                   SET EntryLevel = @EntryLevel
                   WHERE UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", student.UserID),
                new SqlParameter("@EntryLevel", student.EntryLevel)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        // Xóa Student
        public int DeleteStudent(string userID)
        {
            string sql = "DELETE FROM Students WHERE UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            finally
            {
                Disconnect();
            }
        }
        //Taoj MA HOC VIEN tu dong 
        public string GenerateStudentID()
        {
            string sql = "SELECT TOP 1 StudentID FROM Students ORDER BY StudentID DESC";
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                if (reader.Read())
                {
                    string lastID = reader["StudentID"].ToString(); // ví dụ: S005
                    int num = int.Parse(lastID.Substring(1)); // lấy 005 => 5
                    return "S" + (num + 1).ToString("D3"); // => S006
                }
                else
                {
                    return "S001";
                }
            }
            finally
            {
                Disconnect();
            }
        }


    }
}
