using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.System.CloudStorage
{
    public class UploadSignedUrlGetRequest
    {
        public string FolderName { get; set; }
        public string Id { get; set; }
        public string FileName { get; set; }
    }
}