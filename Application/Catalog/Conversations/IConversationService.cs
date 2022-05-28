using MessageRoomSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Catalog.Conversations;
using ViewModel.Common;

namespace Application.Catalog.Conversations
{
    public interface IConversationService
    {
        Task<ApiResult<PagedResult<ConversationViewModel>>> GetAllPaging(ConversationPagingRequest request);

        Task<ApiResult<ConversationViewModel>> GetById(ConversationGetRequest request, int id);

        Task<ApiResult<ConversationViewModel>> Create(ConversationCreateRequest request);

        Task<ApiResult<bool>> Delete(ConversationDeleteRequest request);

        Task DeleteConversation(Conversation conversation);

        Task<ApiResult<bool>> Update(ConversationUpdateRequest request);
    }
}