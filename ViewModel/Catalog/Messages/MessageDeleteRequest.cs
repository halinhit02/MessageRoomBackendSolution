using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Messages
{
    public class MessageDeleteRequest : BaseMessageRequest
    {
        public int Id { get; set; }
    }
}