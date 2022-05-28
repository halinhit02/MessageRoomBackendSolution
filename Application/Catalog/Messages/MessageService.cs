using Application.Catalog.Attachments;
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
using ViewModel.Catalog.Messages;
using ViewModel.Common;

namespace Application.Catalog.Messages
{
    public class MessageService : IMessageService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly IAttachmentService mAttachmentService;

        public MessageService(MessageRoomDbContext context, IAttachmentService attachmentService)
        {
            mContext = context;
            mAttachmentService = attachmentService;
        }

        public async Task<ApiResult<MessageViewModel>> Create(MessageCreateRequest request)
        {
            if (string.IsNullOrEmpty(request.Content) && (request.FileUrls == null || request.FileUrls.Count == 0))
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.NotExistContent);
            }
            var conversation = await mContext.Conversations
                .Where(x => !x.IsDeleted && x.Id == request.ConversationId)
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.NotExistConversation);
            }
            var participant = mContext.Participants.Where(x => x.UserId == request.UserId
            && x.IsJoined && x.ConversationId == request.ConversationId)
                .FirstOrDefault();
            if (participant == null)
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.CommonError);
            }
            var message = new Message()
            {
                ConversationId = request.ConversationId,
                UserId = request.UserId,
                Content = request.Content ?? "",
                MessageType = request.MessageType,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            mContext.Messages.Add(message);
            conversation.UpdatedAt = DateTime.Now;
            mContext.Conversations.Update(conversation);
            bool isSaved = await mContext.SaveChangesAsync() > 0;
            if (!isSaved)
            {
                return new ApiErrorResult<MessageViewModel>();
            }
            var attachments = await mAttachmentService.CreateAll(message.Id, request.FileUrls);
            return new ApiSuccessResult<MessageViewModel>(GetMessageViewModel(message, attachments));
        }

        public async Task<ApiResult<MessageViewModel>> GetById(BaseMessageRequest request, int id)
        {
            var conversation = await mContext.Conversations
                .Where(x => !x.IsDeleted && x.Id == request.ConversationId)
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.NotExistConversation);
            }
            var query = (from par in mContext.Participants
                         where par.ConversationId == request.ConversationId && par.UserId == request.UserId && par.IsJoined
                         select par).ToList();
            if (query.Count() < 1)
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.CommonError);
            }
            var message = await mContext.Messages.Where(x => x.Id == id && x.ConversationId == request.ConversationId && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (message == null)
            {
                return new ApiErrorResult<MessageViewModel>(ResultConstants.NotExistMessage);
            }
            var attachments = await mAttachmentService.GetAllById(message.Id);
            return new ApiSuccessResult<MessageViewModel>(GetMessageViewModel(message, attachments));
        }

        public async Task<ApiResult<bool>> Delete(MessageDeleteRequest request)
        {
            var conversation = await mContext.Conversations
                .Where(x => !x.IsDeleted && x.Id == request.ConversationId)
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NotExistConversation);
            }
            var message = await mContext.Messages
                .Where(x => x.Id == request.Id && x.ConversationId == request.ConversationId
                && x.UserId == request.UserId)
                .FirstOrDefaultAsync();
            if (message == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.CommonError);
            }
            message.IsDeleted = true;
            message.DeletedAt = DateTime.Now;
            mContext.Messages.Update(message);
            await mAttachmentService.DeleteAll(message.Id);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task DeleteAll(int conversationId)
        {
            var attachments = await (from mess in mContext.Messages
                                     where mess.ConversationId == conversationId && !mess.IsDeleted
                                     join attachment in mContext.Attachments on mess.Id equals attachment.MessageId
                                     where !attachment.IsDeleted
                                     select attachment).ToListAsync();
            foreach (var attachment in attachments)
            {
                await mAttachmentService.Delete(attachment);
            }
        }

        public async Task<ApiResult<PagedResult<MessageViewModel>>> GetAllPaging(MessagePagingRequest request)
        {
            var conversation = await mContext.Conversations
                .Where(x => !x.IsDeleted && x.Id == request.ConversationId)
                .FirstOrDefaultAsync();
            if (conversation == null)
            {
                return new ApiErrorResult<PagedResult<MessageViewModel>>(ResultConstants.NotExistConversation);
            }
            var query = (from par in mContext.Participants
                         where par.ConversationId == request.ConversationId && par.IsJoined && par.UserId == request.UserId
                         select par).ToList();
            if (query.Count() < 1)
            {
                return new ApiErrorResult<PagedResult<MessageViewModel>>(ResultConstants.CommonError);
            }

            var messages = (from x in mContext.Messages
                            where x.ConversationId == request.ConversationId && !x.IsDeleted
                            select x).ToList();
            var totalRecord = messages.Count();
            var takenMessage = messages.OrderBy(x => x.CreatedAt)
                .SkipLast(request.PageSize * (request.PageIndex - 1))
                .TakeLast(request.PageSize)
                .ToList();
            var data = new List<MessageViewModel>();
            foreach (var message in takenMessage)
            {
                var attachments = await mAttachmentService.GetAllById(message.Id);
                data.Add(GetMessageViewModel(message, attachments));
            }
            var pageResult = new PagedResult<MessageViewModel>()
            {
                TotalRecord = totalRecord,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<MessageViewModel>>(pageResult);
        }

        public async Task<MessageViewModel> GetLatestMessage(int ConversationId)
        {
            var latestMessage = mContext.Messages
                .Where(x => x.ConversationId == ConversationId && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();
            if (latestMessage == null)
            {
                return null;
            }
            var attachments = await mAttachmentService.GetAllById(latestMessage.Id);
            var content = latestMessage.Content;
            if (attachments != null && string.IsNullOrEmpty(latestMessage.Content))
            {
                if (latestMessage.MessageType == 1)
                {
                    content = $"Đã gửi {attachments.Count()} hình ảnh";
                }
                else if (latestMessage.MessageType == 2)
                {
                    content = $"Đã gửi video";
                }
                else if (latestMessage.MessageType == 3)
                {
                    content = $"Đã gửi tệp âm thanh";
                }
                else if (latestMessage.MessageType == 4)
                {
                    content = $"Đã gửi tệp tin";
                }
            }
            return new MessageViewModel()
            {
                Id = latestMessage.Id,
                SenderId = latestMessage.UserId,
                Content = content,
                MessageType = latestMessage.MessageType,
                ConversationId = latestMessage.ConversationId,
                IsDeleted = latestMessage.IsDeleted,
                CreatedAt = latestMessage.CreatedAt,
                DeletedAt = latestMessage.DeletedAt,
                UpdatedAt = latestMessage.UpdatedAt
            }; ;
        }

        public static MessageViewModel GetMessageViewModel(Message message, List<AttachmentViewModel> attachments)
        {
            return new MessageViewModel()
            {
                Id = message.Id,
                SenderId = message.UserId,
                Content = message.Content,
                MessageType = message.MessageType,
                Attachments = attachments,
                ConversationId = message.ConversationId,
                IsDeleted = message.IsDeleted,
                CreatedAt = message.CreatedAt,
                DeletedAt = message.DeletedAt,
                UpdatedAt = message.UpdatedAt
            };
        }
    }
}