using System;

namespace YouthActionDotNet.Models
{
    public class User
    {
        public User () {
            this.UserId = Guid.NewGuid().ToString();
        }
        public string UserId { get; set; }
        public string username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }

    }
}