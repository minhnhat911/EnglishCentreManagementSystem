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
    public class LoginDL:DataProvider
    {
        public bool Login(Account account)
        {
            string sql = "SELECT COUNT(*) FROM Accounts WHERE Username = @Username AND PasswordHash = @PasswordHash AND IsActive = 1";
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@Username", account.Username),
            new SqlParameter("@PasswordHash", account.PasswordHash)
        };
            try
            {
                Connect();
                return ((int)MyExcuteScalar(sql, CommandType.Text, parameters) > 0);
            }
            finally { Disconnect(); }
        }      

        public void UpdateLastLogin(int accountId)
        {
            string sql = "UPDATE Accounts SET LastLogin = @LastLogin WHERE AccountID = @AccountID";
            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@LastLogin", DateTime.Now),
            new SqlParameter("@AccountID", accountId)
        };
            try
            {
                Connect();
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            finally { Disconnect(); }
        }
        ////////////////////////////////////////
        public Account GetAccountByUsername(string username)
        {
            string sql = @" SELECT a.AccountID, a.UserID, a.Username, a.PasswordHash, a.RoleID, a.IsActive, a.LastLogin,
                                   r.RoleName
                            FROM Accounts a
                            JOIN Roles r ON a.RoleID = r.RoleID
                            WHERE a.Username = @Username";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Username", username)
            };

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text, parameters);

                if (reader.Read())
                {
                    Account acc = new Account
                    {
                        AccountID = (int)reader["AccountID"],
                        UserID = reader["UserID"].ToString(),
                        Username = reader["Username"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        RoleID = (int)reader["RoleID"],
                        IsActive = (bool)reader["IsActive"],
                        LastLogin = reader["LastLogin"] == DBNull.Value ? null : (DateTime?)reader["LastLogin"],
                        Role = new Role
                        {
                            RoleID = (int)reader["RoleID"],
                            RoleName = reader["RoleName"].ToString()
                        }
                    };

                    reader.Close(); // đóng reader khi xong
                    return acc;
                }

                reader.Close();
                return null;
            }
            finally { Disconnect(); }
        }
        

    }
}
