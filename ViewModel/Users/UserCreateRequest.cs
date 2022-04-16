using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Users
{
    public class UserCreateRequest
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public int BranchId { get; set; }
        public int CreatorId { get; set; }
    }
}