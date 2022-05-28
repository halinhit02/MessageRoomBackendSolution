using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Attachments
{
    public class AttachmentCreateRequest
    {
        public int MessageId { get; set; }
        public string ConversationId { get; set; }
        public string ThumbContent { get; set; }
        public string FileContent { get; set; } = "";
    }
}