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
    public class UserDL:DataProvider
    {
        public User GetUserByID(string userId)
        {
            string sql = "SELECT * FROM Users WHERE UserID = @UserID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text, parameters);
                if (reader.Read())
                {
                    return new User
                    {
                        UserID = reader["UserID"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        CreatedAt = reader["CreatedAt"] == DBNull.Value ? null : (DateTime?)reader["CreatedAt"]
                    };


                }
                return null;
            }
            finally
            {
                Disconnect();
            }
        }
        public int AddUser(User user)
        {
            string sql = @"INSERT INTO Users (UserID, LastName, FirstName, Gender, DateOfBirth, Email, Phone, Address, CreatedAt)
                         VALUES (@UserID, @LastName, @FirstName, @Gender, @DateOfBirth, @Email, @Phone, @Address, @CreatedAt)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@DateOfBirth", user.DateOfBirth),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@Address", user.Address),
                new SqlParameter("@CreatedAt", user.CreatedAt)
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
        public string GenerateUserID()
        {
            string sql = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                if (reader.Read())
                {
                    string lastID = reader["UserID"].ToString(); // ví dụ: U005
                    int num = int.Parse(lastID.Substring(2)); // lấy 005 => 5
                    return "U" + (num + 1).ToString("D3"); // => US006
                }
                else
                {
                    return "U001";
                }
            }
            finally
            {
                Disconnect();
            }
        }
        public List<Role> GetAllRoles()
        {
            string sql = "SELECT RoleID, RoleName FROM Roles"; // Truy vấn các RoleID và RoleName
            List<Role> roles = new List<Role>();

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);

                while (reader.Read())
                {
                    Role role = new Role
                    {
                        RoleID = Convert.ToInt32(reader["RoleID"]),
                        RoleName = reader["RoleName"].ToString()
                    };
                    roles.Add(role);
                }
            }
            finally
            {
                Disconnect();
            }

            return roles;
        }
        public int UpdateUser(User user)
        {
            string sql = @"UPDATE Users
                       SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, 
                           DateOfBirth = @DateOfBirth, Email = @Email, Phone = @Phone, 
                           Address = @Address
                       WHERE UserID = @UserID";

            List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@UserID", user.UserID),
            new SqlParameter("@FirstName", user.FirstName),
            new SqlParameter("@LastName", user.LastName),
            new SqlParameter("@Gender", user.Gender),
            new SqlParameter("@DateOfBirth", user.DateOfBirth),
            new SqlParameter("@Email", user.Email),
            new SqlParameter("@Phone", user.Phone),
            new SqlParameter("@Address", user.Address)
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

        // Xóa User
        public int DeleteUser(string userID)
        {
            string sql = "DELETE FROM Users WHERE UserID = @UserID";

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
    }
}
