using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Catalog.Conversations
{
    public class ConversationPagingRequest : BasePagingRequest
    {
        public int UserId { get; set; }
    }
}