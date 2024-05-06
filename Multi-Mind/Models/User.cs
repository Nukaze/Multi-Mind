using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Mind.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public void SetAll(string username, string email, string hashedPassword)
        {
            this.Username = username;
            this.Email = email;
            this.HashedPassword = hashedPassword;
        }

        public bool IsAnyPropertyEmpty()
        {
            string[] property = [this.Username, this.Email, this.HashedPassword];

            if (property.Any(p => string.IsNullOrEmpty(p)))
            {
                return true;
            }

            return false;
        }
    }
}
