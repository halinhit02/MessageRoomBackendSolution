using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Participants
{
    public class ParticipantCreateRequest : BaseParticipantRequest
    {
        public int JoinerId { get; set; }
    }
}