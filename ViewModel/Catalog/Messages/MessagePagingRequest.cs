using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Catalog.Messages
{
    public class MessagePagingRequest : BasePagingRequest
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}