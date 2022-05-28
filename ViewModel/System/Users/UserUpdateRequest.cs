using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.System.Users
{
    public class UserUpdateRequest : BaseUserRequest
    {
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
    }
}