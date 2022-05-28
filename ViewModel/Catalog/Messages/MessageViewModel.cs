using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Catalog.Attachments;

namespace ViewModel.Catalog.Messages
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int MessageType { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}