using SQLite;

namespace Multi_Mind.Models
{
    
    [Table("Users")]
    public class User
    {

        [Column("uid")]
        [PrimaryKey]
        public string Uid { get; set; }

        [Column("username")]
        public string Username { get; set; }

        
        [Column("email")]
        [Unique]
        public string Email { get; set; }

        [Column("password")]
        public string HashedPassword { get; set; }

        public void SetAll(string username, string email, string hashedPassword)
        {
            Username = username;
            Email = email;
            HashedPassword = hashedPassword;
        }

        public bool IsAnyPropertyEmpty()
        {
            string[] property = [Username, Email, HashedPassword];

            if (property.Any(p => string.IsNullOrEmpty(p)))
            {
                return true;
            }

            return false;
        }

    }
}
