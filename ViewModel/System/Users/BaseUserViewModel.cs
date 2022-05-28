using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.System.Users
{
    public class BaseUserViewModel
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public int BranchId { get; set; }
        public int CreatorId { get; set; }
        public DateTime LastAccess { get; set; }
    }
}