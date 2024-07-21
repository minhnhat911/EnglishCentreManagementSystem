using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace BusinessLayer
{
    public class UserBL
    {
        private UserDL userDL;
        public UserBL()
        {
            userDL = new UserDL();
        }
        public User GetUserByID(string userId)
        {
            try
            {
                return userDL.GetUserByID(userId);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int AddUser(User user)
        {
            try
            {
                return userDL.AddUser(user);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public string GenerateUserID()
        {
            return userDL.GenerateUserID();
        }
        public List<Role> GetAllRoles()
        {
            return userDL.GetAllRoles();  // Gọi phương thức của DataLayer để lấy danh sách roles
        }
        // Cập nhật thông tin User
        public int UpdateUser(User user)
        {
            return userDL.UpdateUser(user); // Gọi phương thức của DataLayer để cập nhật
        }

        // Xóa User
        public int DeleteUser(string userID)
        {
            return userDL.DeleteUser(userID); // Gọi phương thức của DataLayer để xóa
        }
    }
}
