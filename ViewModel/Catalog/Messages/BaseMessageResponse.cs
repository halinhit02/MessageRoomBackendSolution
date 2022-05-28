using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Catalog.Attachments;
using ViewModel.System.Users;

namespace ViewModel.Catalog.Messages
{
    public class BaseMessageResponse
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public BaseUserViewModel Sender { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int MessageType { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}