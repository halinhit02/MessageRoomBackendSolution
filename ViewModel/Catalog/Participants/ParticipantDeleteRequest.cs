using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Participants
{
    public class ParticipantDeleteRequest : BaseParticipantRequest
    {
        public int LeaverId { get; set; }
    }
}