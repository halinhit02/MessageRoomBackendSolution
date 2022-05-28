using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Catalog.Participants;
using ViewModel.Common;

namespace Application.Catalog.Participants
{
    public interface IParticipantService
    {
        public Task<ApiResult<PagedResult<ParticipantViewModel>>> GetAllPaging(ParticipantPagingRequest request);

        public Task<ApiResult<ParticipantViewModel>> Create(ParticipantCreateRequest request);

        public Task<ApiResult<bool>> Delete(ParticipantDeleteRequest request);

        public Task<ApiResult<bool>> Update(ParticipantUpdateRequest request);
    }
}