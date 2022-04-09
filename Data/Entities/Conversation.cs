using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ChannelId { get; set; }
        public string Avatar { get; set; }
        public string Background { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public AppUser User { get; set; }
        public List<Message> Messages { get; set; }
        public List<Participant> Participants { get; set; }
    }
}