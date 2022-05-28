using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Messages
{
    public class BaseMessageRequest
    {
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}