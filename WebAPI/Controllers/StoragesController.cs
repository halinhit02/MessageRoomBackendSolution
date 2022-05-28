using Application.System.CloudStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Common;
using ViewModel.System.CloudStorage;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoragesController : ControllerBase
    {
        private readonly ICloudStorage mCloudStorage;

        public StoragesController(ICloudStorage cloudStorage)
        {
            mCloudStorage = cloudStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetUploadSignedURL([FromQuery] UploadSignedUrlGetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(request.FolderName) ||
                !request.FolderName.Contains("users") && !request.FolderName.Contains("conversations"))
            {
                return Forbid();
            }
            if (!request.FileName.Contains(".") || request.FileName.Contains("/") ||
                request.FolderName.Contains("/"))
            {
                return Ok(new ApiErrorResult<object>(ResultConstants.NotValidAttachment));
            }
            var result = await mCloudStorage.GenerateUploadSignedUrl(request);
            return Ok(result);
        }

        [HttpDelete()]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteFile([FromQuery] string fileUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(fileUrl) || !fileUrl.Contains("."))
            {
                return Ok(new ApiErrorResult<bool>(ResultConstants.NotExistContent));
            }
            var result = await mCloudStorage.DeleteFileAsync(fileUrl);
            if (!result)
            {
                return Ok(new ApiErrorResult<bool>(ResultConstants.CommonError));
            }
            return Ok(new ApiSuccessResult<bool>());
        }
    }
}