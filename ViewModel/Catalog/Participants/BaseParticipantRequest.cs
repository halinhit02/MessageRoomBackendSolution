using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Participants
{
    public class BaseParticipantRequest
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}