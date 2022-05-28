using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Attachments
{
    public class AttachmentUpdateRequest
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string ThumbContent { get; set; }
        public string FileContent { get; set; }
        public bool IsDeleted { get; set; }
    }
}