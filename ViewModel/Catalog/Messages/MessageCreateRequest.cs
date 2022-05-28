using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Messages
{
    public class MessageCreateRequest : BaseMessageRequest
    {
        public string Content { get; set; } = "";
        public int MessageType { get; set; }
        public List<string> FileUrls { get; set; }
    }
}