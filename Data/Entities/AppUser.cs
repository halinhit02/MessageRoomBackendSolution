using MessageRoomSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Entities
{
    public class AppUser
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
        public string Password { get; set; }
        public string Avatar { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentId { get; set; }
        public int DivisionId { get; set; }
        public int BranchId { get; set; }
        public int CreatorId { get; set; }
        public string Token { get; set; }
        public DateTime TokenOn { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Message> Messages { get; set; }
        public List<Conversation> Conversations { get; set; }
        public List<Participant> Participants { get; set; }
    }
}