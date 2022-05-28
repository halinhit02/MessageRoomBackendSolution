using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Catalog.Messages;

namespace ViewModel.Catalog.Conversations
{
    public class ConversationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatorId { get; set; }
        public string Description { get; set; }
        public string ChannelId { get; set; }
        public string Avatar { get; set; }
        public string Background { get; set; }
        public MessageViewModel LatestMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}