using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Conversations
{
    public class ConversationUpdateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Background { get; set; }
    }
}