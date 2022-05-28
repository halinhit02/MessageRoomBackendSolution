using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.System.Users;

namespace ViewModel.Catalog.Participants
{
    public class ParticipantViewModel
    {
        public int ConversationId { get; set; }
        public BaseUserViewModel User { get; set; }
        public string NickName { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}