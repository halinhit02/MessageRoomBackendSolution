using MessageRoomSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Entities
{
    public class Participant
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string NickName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsJoined { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Conversation Conversation { get; set; }
        public AppUser User { get; set; }
    }
}