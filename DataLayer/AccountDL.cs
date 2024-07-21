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
    public class AccountDL : DataProvider
    {
        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            string proc = "sp_GetAllAccounts";

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(proc, CommandType.StoredProcedure);

                while (reader.Read())
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
                    accounts.Add(acc);
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

            return accounts;
        }
            public Account GetAccountByUserID(string userID)
            {
                string procName = "sp_GetAccountByUserID";
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
                        Account account = new Account
                        {
                            AccountID = Convert.ToInt32(reader["AccountID"]),
                            UserID = reader["UserID"].ToString(),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            RoleID = Convert.ToInt32(reader["RoleID"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            LastLogin = reader["LastLogin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLogin"]),
                            Role = new Role
                            {
                                RoleID = (int)reader["RoleID"],
                                RoleName = reader["RoleName"].ToString()
                            }
                        };
                        reader.Close();
                        return account;
                    }

                    reader.Close();
                    return null;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi lấy thông tin tài khoản theo UserID.", ex);
                }
                finally
                {
                    Disconnect();
                }
        }

        public int AddAccount(Account acc)
        {
            string proc = "sp_AddAccount";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@UserID", acc.UserID),
        new SqlParameter("@Username", acc.Username),
        new SqlParameter("@PasswordHash", acc.PasswordHash),
        new SqlParameter("@RoleID", acc.Role.RoleID),
        new SqlParameter("@IsActive", acc.IsActive)
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

        public int UpdateAccount(Account account)
        {
            string proc = "sp_UpdateAccount";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@UserID", account.UserID),
        new SqlParameter("@Username", account.Username),
        new SqlParameter("@PasswordHash", account.PasswordHash),
        new SqlParameter("@RoleID", account.Role.RoleID),
        new SqlParameter("@IsActive", account.IsActive)
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

        public int DeleteAccount(string userID)
        {
            string proc = "sp_DeleteAccount";

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

    }
}
