using MessageRoomSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Catalog.Attachments;
using ViewModel.Common;

namespace Application.Catalog.Attachments
{
    public interface IAttachmentService
    {
        public Task<ApiResult<AttachmentViewModel>> Get(AttachmentGetRequest request);

        public Task<List<AttachmentViewModel>> GetAllById(int MessageId);

        public Task<AttachmentViewModel> Create(int messageId, string fileUrl);

        public Task<List<AttachmentViewModel>> CreateAll(int messageId, List<string> fileUrls);

        public Task<ApiResult<bool>> DeleteById(AttachmentDeleteRequest request);

        public Task Delete(Attachment attachment);

        public Task<bool> DeleteAll(int messageId);
    }
}