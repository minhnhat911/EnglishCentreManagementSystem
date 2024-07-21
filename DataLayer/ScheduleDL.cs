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
    public class ScheduleDL:DataProvider
    {
        // Lấy tất cả thời khóa biểu của các lớp
        public List<Schedule> GetAllSchedules()
        {
            string procName = "sp_GetAllSchedules";  // Stored procedure đã tạo

            List<Schedule> schedules = new List<Schedule>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(procName, CommandType.StoredProcedure);
                while (reader.Read())
                {
                    Schedule schedule = new Schedule
                    {
                        ScheduleID = reader["ScheduleID"].ToString(),
                        ClassID = reader["ClassID"].ToString(),
                        ClassDate = (DateTime)reader["ClassDate"],
                        StartTime = (TimeSpan)reader["StartTime"],
                        EndTime = (TimeSpan)reader["EndTime"],
                        Room = reader["Room"].ToString()
                    };

                    schedules.Add(schedule);
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

            return schedules;
        }

        // Thêm thời khóa biểu mới
        public int AddSchedule(Schedule schedule)
        {
            string procName = "sp_AddSchedule";  // Stored procedure đã tạo

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ScheduleID", schedule.ScheduleID),
                new SqlParameter("@ClassID", schedule.ClassID),
                new SqlParameter("@ClassDate", schedule.ClassDate),
                new SqlParameter("@StartTime", schedule.StartTime),
                new SqlParameter("@EndTime", schedule.EndTime),
                new SqlParameter("@Room", schedule.Room)
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

        // Cập nhật thời khóa biểu
        public int UpdateSchedule(Schedule schedule)
        {
            string procName = "sp_UpdateSchedule";  // Stored procedure đã tạo

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ScheduleID", schedule.ScheduleID),
                new SqlParameter("@ClassID", schedule.ClassID),
                new SqlParameter("@ClassDate", schedule.ClassDate),
                new SqlParameter("@StartTime", schedule.StartTime),
                new SqlParameter("@EndTime", schedule.EndTime),
                new SqlParameter("@Room", schedule.Room)
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

        // Xóa thời khóa biểu
        public int DeleteSchedule(string scheduleID)
        {
            string procName = "sp_DeleteSchedule";  // Stored procedure đã tạo

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ScheduleID", scheduleID)
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
    }
}
