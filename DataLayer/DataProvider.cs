using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;


namespace DataLayer
{
    public class DataProvider
    {
        private SqlConnection cn;
        SqlCommand cmd;
        //Chuỗi kết nối dùng bằng appconfig
        public DataProvider() 
        {
            string connStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(connStr);
        }
        public void Connect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Closed)
                    cn.Open();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public object MyExcuteScalar(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;

            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public SqlDataReader MyExcuteReader(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;

            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int MyExcuteNonQuery(string sql, CommandType type, List<SqlParameter> parameters = null) //Đây là hàm đọc sql không trả về gì vd như : thêm,xem, xóa, sửa, 
        {
            cmd = new SqlCommand(sql, cn);
            cmd.CommandType = type;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                return (cmd.ExecuteNonQuery());
            }
            catch (SqlException ex) 
            { 
                throw ex;
            }
        }
    }
}
