using Application.Catalog.Conversations;
using MessageRoomSolution.Data.EF;
using MessageRoomSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Catalog.Participants;
using ViewModel.Common;
using ViewModel.System.Users;

namespace Application.Catalog.Participants
{
    public class ParticipantService : IParticipantService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly IConversationService mConversationService;

        public ParticipantService(MessageRoomDbContext context, IConversationService conversationService)
        {
            mContext = context;
            mConversationService = conversationService;
        }

        public async Task<ApiResult<ParticipantViewModel>> Create(ParticipantCreateRequest request)
        {
            // check user requested is a participant
            var participants = mContext.Participants
                .Where(x => x.ConversationId == request.ConversationId)
                .ToList();
            var participant = participants.Where(x => x.UserId == request.UserId && x.IsJoined)
                .FirstOrDefault();
            if (participant == null)
            {
                return new ApiErrorResult<ParticipantViewModel>(ResultConstants.CommonError);
            }
            // check joiner exsisted
            var user = await mContext.AppUsers
               .Where(x => x.Id == request.JoinerId && !x.IsDeleted && !x.IsLocked)
               .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<ParticipantViewModel>(ResultConstants.NotExistUser);
            }
            // create new participant
            var mParticipant = participants.Where(x => x.UserId == request.JoinerId)
                .FirstOrDefault();
            if (mParticipant == null)
            {
                mParticipant = new Participant()
                {
                    UserId = request.JoinerId,
                    IsAdmin = false,
                    IsJoined = true,
                    ConversationId = request.ConversationId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                mContext.Participants.Add(mParticipant);
            }
            else
            {
                mParticipant.IsJoined = true;
                mParticipant.UpdatedAt = DateTime.Now;
                mContext.Participants.Update(mParticipant);
            }
            var isSaved = await mContext.SaveChangesAsync() > 0;
            if (!isSaved)
            {
                return new ApiErrorResult<ParticipantViewModel>();
            }
            return new ApiSuccessResult<ParticipantViewModel>(GetParticipantViewModel(user, mParticipant));
        }

        public async Task<ApiResult<PagedResult<ParticipantViewModel>>> GetAllPaging(ParticipantPagingRequest request)
        {
            var mParticipant = await mContext.Participants
                .Where(x => x.ConversationId == request.ConversationId && x.UserId == request.UserId && x.IsJoined)
                .FirstOrDefaultAsync();
            if (mParticipant == null)
            {
                return new ApiErrorResult<PagedResult<ParticipantViewModel>>(ResultConstants.CommonError);
            }
            var takenData = await (from participant in mContext.Participants
                                   where participant.ConversationId == request.ConversationId && participant.IsJoined
                                   join user in mContext.AppUsers on participant.UserId equals user.Id
                                   where !user.IsDeleted && !user.IsLocked
                                   select new { participant, user }).ToListAsync();
            var totalRecord = takenData.Count();
            if (request.PageIndex != -1)
            {
                takenData = takenData.Skip(request.PageSize * (request.PageIndex - 1))
                .Take(request.PageSize)
                .ToList();
            }
            var data = (from x in takenData
                        select GetParticipantViewModel(x.user, x.participant))
                        .ToList();

            var pageResult = new PagedResult<ParticipantViewModel>()
            {
                TotalRecord = totalRecord,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ParticipantViewModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> Delete(ParticipantDeleteRequest request)
        {
            var participants = (from x in mContext.Participants
                                where x.ConversationId == request.ConversationId && x.IsJoined
                                select x).ToList();

            var mParticipant = participants.Where(x => x.UserId == request.LeaverId).FirstOrDefault();
            if (mParticipant == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.CommonError);
            }
            if (request.LeaverId != request.UserId)
            {
                var admins = participants.Where(x => x.IsAdmin).ToList();
                var participant = admins.Where(x => x.UserId == request.UserId).FirstOrDefault();
                if (admins.Count() > 0 && participant == null)
                {
                    return new ApiErrorResult<bool>(ResultConstants.NoPermission);
                }
            }
            if (participants.Count() <= 2)
            {
                var conversation = await mContext.Conversations
                    .Where(x => x.Id == request.ConversationId)
                    .FirstOrDefaultAsync();
                await mConversationService.DeleteConversation(conversation);
                return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
            }
            mParticipant.IsJoined = false;
            mParticipant.IsAdmin = false;
            mParticipant.UpdatedAt = DateTime.Now;
            mContext.Participants.Update(mParticipant);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task<ApiResult<bool>> Update(ParticipantUpdateRequest request)
        {
            var participants = (from x in mContext.Participants
                                where x.ConversationId == request.ConversationId && x.IsJoined
                                select x).ToList();

            var mParticipant = participants.Where(x => x.UserId == request.OtherId).FirstOrDefault();
            if (mParticipant == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.CommonError);
            }

            var admins = participants.Where(x => x.IsAdmin).ToList();
            var par = admins.Where(x => x.UserId == request.UserId).FirstOrDefault();
            if (admins.Count() > 0 && par == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NoPermission);
            }

            if (!string.IsNullOrEmpty(request.NickName))
            {
                mParticipant.NickName = request.NickName;
            }
            mParticipant.IsAdmin = request.IsAdmin;
            mParticipant.UpdatedAt = DateTime.Now;
            mContext.Participants.Update(mParticipant);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public static ParticipantViewModel GetParticipantViewModel(AppUser user, Participant participant)
        {
            return new ParticipantViewModel()
            {
                ConversationId = participant.ConversationId,
                IsAdmin = participant.IsAdmin,
                NickName = participant.NickName,
                User = GetBaseUserViewModel(user),
                CreatedAt = participant.CreatedAt,
                UpdatedAt = participant.UpdatedAt
            };
        }

        public static BaseUserViewModel GetBaseUserViewModel(AppUser user)
        {
            var userVM = new BaseUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                DivisionId = user.DivisionId,
                Avatar = user.Avatar,
                BranchId = user.BranchId,
                Gender = user.Gender,
                DepartmentId = user.DepartmentId,
                StaffId = user.StaffId,
                CreatorId = user.CreatorId,
                LastAccess = user.LastAccess
            };
            return userVM;
        }
    }
}