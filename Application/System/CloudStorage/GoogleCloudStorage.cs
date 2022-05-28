using Google;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Common;
using ViewModel.System.CloudStorage;

namespace Application.System.CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly string BASE_URL = "https://storage.googleapis.com";
        private readonly GoogleCredential googleCredential;
        private readonly UrlSigner urlSigner;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            string credentialFilePath = configuration.GetValue<string>(SystemConstants.CredentialFile);
            googleCredential = GoogleCredential.FromFile(credentialFilePath);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
            urlSigner = UrlSigner.FromServiceAccountPath(credentialFilePath);
        }

        public async Task<bool> DeleteFileAsync(string fileURL)
        {
            try
            {
                string fileNameInStorage = fileURL.Replace($"{BASE_URL}/{bucketName}/", "");
                await storageClient.DeleteObjectAsync(bucketName, fileNameInStorage);
                return true;
            }
            catch (GoogleApiException e)
            {
                if (e.HttpStatusCode.Equals(404))
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            string fileName = $"{folderName}/{file.FileName}";
            using var stream = file.OpenReadStream();
            var dataObject = await storageClient.UploadObjectAsync(bucketName, fileName, file.ContentType, stream);
            return $"{BASE_URL}/{dataObject.Bucket}/{fileName}";
        }

        public string GetFileURL(string filePath)
        {
            return $"{BASE_URL}/{bucketName}/{filePath}";
        }

        public async Task<ApiSuccessResult<SignedUrlViewModel>> GenerateUploadSignedUrl(UploadSignedUrlGetRequest request)
        {
            var filePath = $"{request.FolderName}/{request.Id}/{request.FileName}";
            // V4 is the default signing version.
            UrlSigner.Options options = UrlSigner.Options.FromDuration(TimeSpan.FromHours(1));
            UrlSigner.RequestTemplate template = UrlSigner.RequestTemplate
                .FromBucket(bucketName)
                .WithObjectName(filePath)
                .WithHttpMethod(HttpMethod.Put);
            var signedUrl = await urlSigner.SignAsync(template, options);
            var signedUrlVM = new SignedUrlViewModel()
            {
                SignedUrl = signedUrl,
                FileUrl = GetFileURL(filePath)
            };
            return new ApiSuccessResult<SignedUrlViewModel>(signedUrlVM);
        }

        public string GetIconUrl(string fileUrl)
        {
            var fileName = Path.GetFileName(fileUrl).Trim('"');
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string type);
            string contentType = type ?? "text/plain";
            if (contentType.Contains("word"))
            {
                return GetFileURL($"icons/word.png");
            }
            if (contentType.Contains("spreadsheetml.sheet") || contentType.Contains("excel"))
            {
                return GetFileURL($"icons/excel.png");
            }
            if (contentType.Contains("presentation"))
            {
                return GetFileURL($"icons/powerpoint.png");
            }
            if (contentType.Contains("application/pdf"))
            {
                return GetFileURL($"icons/pdf.png");
            }
            if (contentType.Contains("video"))
            {
                return GetFileURL($"icons/video.png");
            }
            if (contentType.Contains("image"))
            {
                return GetFileURL($"icons/image.png");
            }
            if (contentType.Contains("audio"))
            {
                return GetFileURL($"icons/music.png");
            }
            if (contentType.Contains("zip") || contentType.Contains("octet-stream"))
            {
                return GetFileURL($"icons/zip.png");
            }
            return GetFileURL($"icons/text.png");
        }
    }
}