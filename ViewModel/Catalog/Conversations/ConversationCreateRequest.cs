using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Conversations
{
    public class ConversationCreateRequest
    {
        public IList<int> UserIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}