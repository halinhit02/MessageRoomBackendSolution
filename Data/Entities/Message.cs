using MessageRoomSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int MessageType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Conversation Conversation { get; set; }
        public AppUser User { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}