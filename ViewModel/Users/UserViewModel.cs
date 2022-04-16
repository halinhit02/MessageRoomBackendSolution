using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string AboutMe { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Avatar { get; set; }
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public int BranchId { get; set; }
        public int CreatorId { get; set; }
        public DateTime LastAccess { get; set; }
    }
}