using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Common;

namespace ViewModel.Catalog.Participants
{
    public class ParticipantPagingRequest : BasePagingRequest
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
    }
}