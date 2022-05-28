using Application.System.CloudStorage;
using MessageRoomSolution.Data.EF;
using MessageRoomSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Catalog.Attachments;
using ViewModel.Common;

namespace Application.Catalog.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly ICloudStorage mCloudStorage;

        public AttachmentService(MessageRoomDbContext context, ICloudStorage cloudStorage)
        {
            mContext = context;
            mCloudStorage = cloudStorage;
        }

        public AttachmentService(MessageRoomDbContext context)
        {
            mContext = context;
        }

        public async Task<ApiResult<AttachmentViewModel>> Get(AttachmentGetRequest request)
        {
            var attachment = await mContext.Attachments.FindAsync(request.Id);
            if (attachment == null || attachment.IsDeleted)
            {
                return new ApiErrorResult<AttachmentViewModel>(ResultConstants.NotExistAttachment);
            }
            return new ApiSuccessResult<AttachmentViewModel>(GetAttachmentVM(attachment));
        }

        public async Task<List<AttachmentViewModel>> GetAllById(int messageId)
        {
            var attachments = await (from attachment in mContext.Attachments
                                     where attachment.MessageId == messageId
                                     select attachment).ToListAsync();
            if (attachments.Count == 0)
            {
                return null;
            }
            var attachmentVmList = (from x in attachments
                                    select GetAttachmentVM(x))
                                   .ToList();
            return attachmentVmList;
        }

        public async Task<AttachmentViewModel> Create(int messageId, string fileUrl)
        {
            var attachment = new Attachment()
            {
                MessageId = messageId,
                IsDeleted = false,
                FileUrl = fileUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            mContext.Attachments.Add(attachment);
            var isSaved = await mContext.SaveChangesAsync() > 0;
            if (!isSaved)
            {
                return null;
            }
            return GetAttachmentVM(attachment);
        }

        public async Task<List<AttachmentViewModel>> CreateAll(int messageId, List<string> fileUrls)
        {
            var newAttachments = new List<AttachmentViewModel>();
            if (fileUrls != null && fileUrls.Count > 0)
            {
                foreach (var fileUrl in fileUrls)
                {
                    if (!string.IsNullOrEmpty(fileUrl))
                    {
                        var attachment = await Create(messageId, fileUrl);
                        if (attachment != null)
                        {
                            newAttachments.Add(attachment);
                        }
                    }
                }
            }
            return newAttachments;
        }

        public async Task<ApiResult<bool>> DeleteById(AttachmentDeleteRequest request)
        {
            var message = await mContext.Messages.FindAsync(request.MessageId);
            if (message == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NotExistMessage);
            }
            var attachment = await mContext.Attachments.FindAsync(request.Id);
            if (attachment == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NotExistAttachment);
            }
            attachment.IsDeleted = true;
            attachment.DeletedAt = DateTime.Now;
            mContext.Attachments.Update(attachment);
            await mCloudStorage.DeleteFileAsync(attachment.FileUrl);
            return ApiSuccessResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task Delete(Attachment attachment)
        {
            if (attachment == null)
            {
                return;
            }
            attachment.IsDeleted = true;
            attachment.DeletedAt = DateTime.Now;
            if (!string.IsNullOrEmpty(attachment.ThumbUrl))
            {
                await mCloudStorage.DeleteFileAsync(attachment.ThumbUrl);
                attachment.ThumbUrl = null;
            }
            await mCloudStorage.DeleteFileAsync(attachment.FileUrl);
            attachment.FileUrl = "";
            mContext.Attachments.Update(attachment);
            await mContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAll(int messageId)
        {
            var message = await mContext.Messages.FindAsync(messageId);
            if (message == null)
            {
                return false;
            }
            var attachments = await mContext.Attachments
                .Where(x => x.MessageId == messageId)
                .ToListAsync();
            foreach (var attachment in attachments)
            {
                attachment.IsDeleted = true;
                attachment.DeletedAt = DateTime.Now;
                mContext.Attachments.Update(attachment);
                await mCloudStorage.DeleteFileAsync(attachment.FileUrl);
            }
            return true;
        }

        private AttachmentViewModel GetAttachmentVM(Attachment attachment)
        {
            var attachmentVM = new AttachmentViewModel()
            {
                Id = attachment.Id,
                MessageId = attachment.MessageId,
                ThumbUrl = attachment.ThumbUrl ?? mCloudStorage.GetIconUrl(attachment.FileUrl),
                FileUrl = attachment.FileUrl,
                IsDeleted = attachment.IsDeleted,
                CreatedAt = attachment.CreatedAt,
                UpdatedAt = attachment.UpdatedAt,
                DeletedAt = attachment.DeletedAt
            };
            return attachmentVM;
        }
    }
}