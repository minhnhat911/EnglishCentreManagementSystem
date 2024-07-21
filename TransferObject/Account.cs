using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Account
    {
        public int AccountID { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public Role Role { get; set; }
 

        public Account(int accountID, string userID, string username, string passwordHash, int roleID, bool isActive, DateTime? lastLogin, Role role)
        {
            this.AccountID = accountID;
            this.UserID = userID;
            this.Username = username;
            this.PasswordHash = passwordHash;
            this.RoleID = roleID;
            this.IsActive = isActive;
            this.LastLogin = lastLogin;
            this.Role = role;
        }
        
        public Account() { }

    }
}
