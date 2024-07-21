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
    public class LoginBL
    {
        private LoginDL loginDL;
        public LoginBL()
        {
            loginDL = new LoginDL();
        }
        public Account Login(string username, string password)
        {
            Account acc = new Account
            {
                Username = username,
                PasswordHash = password
            };

            if (loginDL.Login(acc))
            {
                Account fullAcc = loginDL.GetAccountByUsername(username);
                if (fullAcc != null)
                {
                    loginDL.UpdateLastLogin(fullAcc.AccountID);
                    return fullAcc;
                }
            }
            return null;
        }
    }
}
