using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.System.Users;

namespace ViewModel.Catalog.Participants
{
    public class ParticipantUpdateRequest : BaseParticipantRequest
    {
        public int OtherId { get; set; }
        public string NickName { get; set; }
        public bool IsAdmin { get; set; }
    }
}