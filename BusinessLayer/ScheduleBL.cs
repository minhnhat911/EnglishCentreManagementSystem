using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransferObject;

namespace BusinessLayer
{
    public class ScheduleBL
    {
        private ScheduleDL scheduleDL = new ScheduleDL();

        // Lấy tất cả thời khóa biểu
        public List<Schedule> GetAllSchedules()
        {
            try
            {
                return scheduleDL.GetAllSchedules();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thời khóa biểu từ cơ sở dữ liệu.", ex);
            }
        }

        // Thêm thời khóa biểu mới
        public int AddSchedule(Schedule schedule)
        {
            try
            {
                return scheduleDL.AddSchedule(schedule);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm thời khóa biểu vào cơ sở dữ liệu.", ex);
            }
        }

        // Cập nhật thời khóa biểu
        public int UpdateSchedule(Schedule schedule)
        {
            try
            {
                return scheduleDL.UpdateSchedule(schedule);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi cập nhật thời khóa biểu.", ex);
            }
        }

        // Xóa thời khóa biểu
        public int DeleteSchedule(string scheduleID)
        {
            try
            {
                return scheduleDL.DeleteSchedule(scheduleID);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa thời khóa biểu.", ex);
            }
        }
    }
}
