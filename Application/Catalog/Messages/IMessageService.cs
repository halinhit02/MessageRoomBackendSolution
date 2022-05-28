using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Catalog.Messages;
using ViewModel.Common;

namespace Application.Catalog.Messages
{
    public interface IMessageService
    {
        public Task<ApiResult<PagedResult<MessageViewModel>>> GetAllPaging(MessagePagingRequest request);

        public Task<ApiResult<MessageViewModel>> Create(MessageCreateRequest request);

        public Task<ApiResult<MessageViewModel>> GetById(BaseMessageRequest request, int id);

        public Task<ApiResult<bool>> Delete(MessageDeleteRequest request);

        public Task DeleteAll(int conversationId);

        public Task<MessageViewModel> GetLatestMessage(int ConversationId);
    }
}