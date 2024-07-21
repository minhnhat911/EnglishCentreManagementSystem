using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class ClassDL : DataProvider
    {
        public List<Class> GetAllClasses()
        {
            string procName = "sp_GetAllClasses";
            List<Class> classes = new List<Class>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(procName, CommandType.StoredProcedure);
                while (reader.Read())
                {
                    Class cls = new Class
                    {
                        ClassID = reader["ClassID"].ToString(),
                        CourseID = reader["CourseID"].ToString(),
                        Room = reader["Room"].ToString(),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        MaxStudents = Convert.ToInt32(reader["MaxStudents"])
                    };
                    classes.Add(cls);
                }
                reader.Close();
            }
            finally
            {
                Disconnect();
            }

            return classes;
        }

        public Class GetClassByID(string classId)
        {
            string procName = "sp_GetClassByID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClassID", classId)
            };

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(procName, CommandType.StoredProcedure, parameters);
                if (reader.Read())
                {
                    Class cls = new Class
                    {
                        ClassID = reader["ClassID"].ToString(),
                        CourseID = reader["CourseID"].ToString(),
                        Room = reader["Room"].ToString(),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        MaxStudents = Convert.ToInt32(reader["MaxStudents"])
                    };
                    reader.Close();
                    return cls;
                }
                reader.Close();
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public int AddClass(Class cls)
        {
            string procName = "sp_AddClass";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClassID", cls.ClassID),
                new SqlParameter("@CourseID", cls.CourseID),
                new SqlParameter("@Room", cls.Room),
                new SqlParameter("@StartDate", cls.StartDate),
                new SqlParameter("@EndDate", cls.EndDate),
                new SqlParameter("@MaxStudents", cls.MaxStudents)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        public int UpdateClass(Class cls)
        {
            string procName = "sp_UpdateClass";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClassID", cls.ClassID),
                new SqlParameter("@CourseID", cls.CourseID),
                new SqlParameter("@Room", cls.Room),
                new SqlParameter("@StartDate", cls.StartDate),
                new SqlParameter("@EndDate", cls.EndDate),
                new SqlParameter("@MaxStudents", cls.MaxStudents)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        public int DeleteClass(string classID)
        {
            string procName = "sp_DeleteClass";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClassID", classID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }

        
        public List<Teacher> GetAssignedTeachers(string classId)
        {
            string sql = @"
                SELECT t.TeacherID, t.UserID, t.DegreeID,
                       u.FirstName, u.LastName, u.Email, u.Phone, u.Address, u.Gender, u.DateOfBirth, u.CreatedAt
                FROM ClassTeachers ct
                JOIN Teachers t ON ct.TeacherID = t.TeacherID
                JOIN Users u ON t.UserID = u.UserID
                WHERE ct.ClassID = @ClassID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ClassID", classId)
            };

            List<Teacher> list = new List<Teacher>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text, parameters);
                while (reader.Read())
                {
                    Teacher teacher = new Teacher
                    {
                        TeacherID = reader["TeacherID"].ToString(),
                        UserID = reader["UserID"].ToString(),
                        DegreeID = reader["DegreeID"].ToString(),
                        User = new User
                        {
                            UserID = reader["UserID"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            CreatedAt = reader["CreatedAt"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["CreatedAt"])
                        }
                    };
                    list.Add(teacher);
                }
                reader.Close();
            }
            finally
            {
                Disconnect();
            }

            return list;
        }


        public string GenerateClassID()
        {
            string sql = "SELECT TOP 1 ClassID FROM Classes ORDER BY ClassID DESC";
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                if (reader.Read())
                {
                    string lastID = reader["ClassID"].ToString(); // VD: CL005
                    int num = int.Parse(lastID.Substring(2));     // Bỏ "CL", lấy "005"
                    return "CL" + (num + 1).ToString("D3");        // Tạo mã mới VD: CL006
                }
                return "CL001"; // Nếu chưa có lớp nào
            }
            finally
            {
                Disconnect();
            }
        }

    }
}
