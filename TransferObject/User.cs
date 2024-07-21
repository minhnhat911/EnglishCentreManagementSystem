using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class User
    {
        public string UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }

        public User() { }

        public User(string userID, string lastName, string firstName, string gender, DateTime dateOfBirth,
                    string email, string phone, string address, DateTime? createdAt)
        {
            UserID = userID;
            LastName = lastName;
            FirstName = firstName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedAt = createdAt;
        }

        // Constructor không cần CreatedAt (vì có default trong SQL)
        public User(string userID, string lastName, string firstName, string gender, DateTime dateOfBirth,
                    string email, string phone, string address)
        {
            UserID = userID;
            LastName = lastName;
            FirstName = firstName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedAt = DateTime.Now; // gán mặc định nếu không truyền vào
        }
    }
}
