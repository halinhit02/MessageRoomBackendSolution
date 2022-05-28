using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Attachments
{
    public class AttachmentDeleteRequest
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
    }
}