using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.System.Users
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}