using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class CourseDL : DataProvider
    {
        public List<Course> GetAllCourses()
        {
            List<Course> list = new List<Course>();
            string sql = "sp_GetAllCourses";

            Connect();
            SqlDataReader reader = MyExcuteReader(sql, CommandType.StoredProcedure);
            while (reader.Read())
            {
                Course course = new Course
                {
                    CourseID = reader["CourseID"].ToString(),
                    CourseName = reader["CourseName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Duration = Convert.ToInt32(reader["Duration"]),
                    Fee = Convert.ToDecimal(reader["Fee"])
                };
                list.Add(course);
            }
            reader.Close();
            Disconnect();
            return list;
        }

        public int AddCourse(Course course)
        {
            string procName = "sp_AddCourse";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@CourseID", course.CourseID),
                new SqlParameter("@CourseName", course.CourseName),
                new SqlParameter("@Description", course.Description),
                new SqlParameter("@Duration", course.Duration),
                new SqlParameter("@Fee", course.Fee)
            };
            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, pars);
            }
            finally
            {
                Disconnect();
            }          
        }

        public int UpdateCourse(Course course)
        {
            string procName = "sp_UpdateCourse";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@CourseID", course.CourseID),
                new SqlParameter("@CourseName", course.CourseName),
                new SqlParameter("@Description", course.Description),
                new SqlParameter("@Duration", course.Duration),
                new SqlParameter("@Fee", course.Fee)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, pars);
            }
            finally
            {
                Disconnect();
            }
        }

        public int DeleteCourse(string courseID)
        {
            string procName = "sp_DeleteCourse";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@CourseID", courseID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery(procName, CommandType.StoredProcedure, pars);
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
