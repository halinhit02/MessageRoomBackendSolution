using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.System.CloudStorage;

namespace Application.System.CloudStorage
{
    public interface ICloudStorage
    {
        public Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);

        public Task<bool> DeleteFileAsync(string fileURL);

        public string GetIconUrl(string fileName);

        public Task<ApiSuccessResult<SignedUrlViewModel>> GenerateUploadSignedUrl(UploadSignedUrlGetRequest request);
    }
}