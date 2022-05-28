using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Catalog.Attachments
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string ThumbUrl { get; set; }
        public string FileUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}