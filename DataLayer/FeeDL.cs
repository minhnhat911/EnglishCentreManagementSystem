using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TransferObject;

namespace DataLayer
{
    public class StudentTuitionFeesDL : DataProvider
    {
        public List<StudentTuitionFee> GetAllStudentTuitionFees()
        {
            List<StudentTuitionFee> list = new List<StudentTuitionFee>();
            string sql = "sp_GetAllStudentTuitionFees";

            Connect();
            SqlDataReader reader = MyExcuteReader(sql, CommandType.StoredProcedure);
            while (reader.Read())
            {
                StudentTuitionFee fee = new StudentTuitionFee
                {
                    TuitionID = Convert.ToInt32(reader["TuitionID"]),
                    StudentID = reader["StudentID"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    ClassID = reader["ClassID"].ToString(),
                    CourseName = reader["CourseName"].ToString(),
                    Fee = Convert.ToDecimal(reader["Fee"]),
                    Status = reader["Status"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
                list.Add(fee);
            }
            reader.Close();
            Disconnect();
            return list;
        }

        public int AddStudentTuitionFee(StudentTuitionFee fee)
        {
            string procName = "sp_AddStudentTuitionFee";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@StudentID", fee.StudentID),
                new SqlParameter("@ClassID", fee.ClassID),
                new SqlParameter("@Fee", fee.Fee),
                new SqlParameter("@Status", fee.Status)
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

        public int UpdateStudentTuitionFee(StudentTuitionFee fee)
        {
            string procName = "sp_UpdateStudentTuitionFee";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@TuitionID", fee.TuitionID),
                new SqlParameter("@Fee", fee.Fee),
                new SqlParameter("@Status", fee.Status)
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

        public int DeleteStudentTuitionFee(int tuitionID)
        {
            string procName = "sp_DeleteStudentTuitionFee";
            List<SqlParameter> pars = new List<SqlParameter>
            {
                new SqlParameter("@TuitionID", tuitionID)
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
