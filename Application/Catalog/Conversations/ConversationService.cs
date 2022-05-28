using Application.Catalog.Messages;
using Application.Catalog.Participants;
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
using ViewModel.Catalog.Conversations;
using ViewModel.Catalog.Participants;
using ViewModel.Common;

namespace Application.Catalog.Conversations
{
    public class ConversationService : IConversationService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly ICloudStorage mCloudStorage;
        private readonly IMessageService mMessageService;

        public ConversationService(MessageRoomDbContext context, ICloudStorage cloudStorage
            , IMessageService messageService)
        {
            mContext = context;
            mCloudStorage = cloudStorage;
            mMessageService = messageService;
        }

        public async Task<ApiResult<PagedResult<ConversationViewModel>>> GetAllPaging(ConversationPagingRequest request)
        {
            var conversations = await (from participant in mContext.Participants
                                       where participant.UserId == request.UserId && participant.IsJoined
                                       join conversation in mContext.Conversations on participant.ConversationId equals conversation.Id
                                       where !conversation.IsDeleted
                                       select conversation).ToListAsync();

            var totalRecord = conversations.Count();
            var cons = conversations
                .OrderByDescending(x => x.UpdatedAt)
                .Skip(request.PageSize * (request.PageIndex - 1))
                .Take(request.PageSize)
                .ToList();
            var data = new List<ConversationViewModel>();
            foreach (var a in cons)
            {
                var cv = await GetConversationVM(request.UserId, a);
                if (cv.LatestMessage != null)
                {
                    data.Add(cv);
                }
            }
            var pagedResult = new PagedResult<ConversationViewModel>()
            {
                TotalRecord = totalRecord,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ConversationViewModel>>(pagedResult);
        }

        public async Task<ApiResult<ConversationViewModel>> GetById(ConversationGetRequest request, int id)
        {
            var conversations = await (from con in mContext.Conversations
                                       where con.Id == id && !con.IsDeleted
                                       join participant in mContext.Participants on con.Id equals participant.ConversationId
                                       where participant.UserId == request.UserId && participant.IsJoined
                                       select con).ToListAsync();
            var conversation = conversations.FirstOrDefault();
            if (conversation == null)
            {
                return new ApiErrorResult<ConversationViewModel>(ResultConstants.CommonError);
            }
            var data = await GetConversationVM(request.UserId, conversation);
            return new ApiSuccessResult<ConversationViewModel>(data);
        }

        public async Task<ApiResult<ConversationViewModel>> Create(ConversationCreateRequest request)
        {
            var parInCons = (await (from con in mContext.Conversations
                                    where !con.IsDeleted
                                    join par in mContext.Participants on con.Id equals par.ConversationId
                                    select par).ToListAsync())
                            .GroupBy(x => x.ConversationId)
                            .OrderBy(x => x.Key);
            int currentId = -1;
            foreach (var parInCon in parInCons)
            {
                if (parInCon.Count().Equals(request.UserIds.Count()))
                {
                    currentId = parInCon.Key;
                    foreach (var par in parInCon)
                    {
                        if (!request.UserIds.Contains(par.UserId))
                        {
                            currentId = -1;
                            break;
                        }
                    }
                }
                if (currentId != -1) break;
            }

            if (currentId != -1)
            {
                var currentCon = await mContext.Conversations.Where(x => x.Id == currentId).FirstOrDefaultAsync();
                return new ApiSuccessResult<ConversationViewModel>(await GetConversationVM(request.UserIds[0], currentCon));
            }

            // default [0] is creator
            var user = await mContext.AppUsers
                .Where(x => request.UserIds.Contains(x.Id) && !x.IsDeleted && !x.IsLocked)
                .ToListAsync();
            if (user == null || user.Count() < 2)
            {
                return new ApiErrorResult<ConversationViewModel>(ResultConstants.NotExistUser);
            }
            //create new conversation
            var conversation = new Conversation()
            {
                Title = request.Title,
                Description = request.Description,
                ChannelId = Guid.NewGuid().ToString(),
                UserId = request.UserIds[0],
                Avatar = "",
                Background = "",
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var participants = new List<Participant>();
            foreach (var participantId in request.UserIds)
            {
                // check exist user
                var participant = await mContext.AppUsers
                .Where(x => x.Id == participantId && !x.IsDeleted && !x.IsLocked)
                .FirstOrDefaultAsync();
                if (participant == null)
                {
                    return new ApiErrorResult<ConversationViewModel>(ResultConstants.NotExistUser);
                };
                //create new Participant
                var newParticipant = new Participant()
                {
                    ConversationId = conversation.Id,
                    IsAdmin = true,
                    IsJoined = true,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UserId = participantId
                };
                participants.Add(newParticipant);
            }
            conversation.Participants = participants;
            mContext.Conversations.Add(conversation);
            var isSaved = await mContext.SaveChangesAsync() > 0;
            if (!isSaved)
            {
                return new ApiErrorResult<ConversationViewModel>(ResultConstants.CommonError);
            }
            return new ApiSuccessResult<ConversationViewModel>(await GetConversationVM(request.UserIds[0], conversation));
        }

        public async Task<ApiResult<bool>> Delete(ConversationDeleteRequest request)
        {
            var conversation = await mContext.Conversations.FindAsync(request.Id);
            if (conversation == null || conversation.IsDeleted)
            {
                return new ApiErrorResult<bool>(ResultConstants.NotExistConversation);
            }
            var participants = await (from x in mContext.Participants
                                      where x.ConversationId == request.Id && x.IsJoined
                                      select x).ToListAsync();

            var admins = participants.Where(x => x.IsAdmin).ToList();
            var adminUser = admins.Where(x => x.UserId == request.UserId).FirstOrDefault();
            //check exist admin and admin is user requested
            if (admins.Count() > 0 && adminUser == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NoPermission);
            }
            await DeleteConversation(conversation);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task<ApiResult<bool>> Update(ConversationUpdateRequest request)
        {
            var conversation = await mContext.Conversations.FindAsync(request.Id);
            if (conversation == null || conversation.IsDeleted)
            {
                return new ApiErrorResult<bool>(ResultConstants.NotExistConversation);
            }
            var participants = await (from x in mContext.Participants
                                      where x.ConversationId == request.Id && x.IsJoined
                                      select x).ToListAsync();
            var admins = participants.Where(x => x.IsAdmin).ToList();
            var adminUser = admins.Where(x => x.UserId == request.UserId).FirstOrDefault();
            //check exist admin and admin is user requested
            if (admins.Count() > 0 && adminUser == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.NoPermission);
            }
            if (!string.IsNullOrEmpty(request.Title))
            {
                conversation.Title = request.Title;
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                conversation.Description = request.Description;
            }
            if (!string.IsNullOrEmpty(request.Avatar))
            {
                if (conversation.Avatar != null)
                {
                    await mCloudStorage.DeleteFileAsync(conversation.Avatar);
                }
                conversation.Avatar = request.Avatar;
            }
            if (!string.IsNullOrEmpty(request.Background))
            {
                if (conversation.Background != null)
                {
                    await mCloudStorage.DeleteFileAsync(conversation.Background);
                }
                conversation.Background = request.Background;
            }
            conversation.UpdatedAt = DateTime.Now;
            mContext.Conversations.Update(conversation);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task DeleteConversation(Conversation conversation)
        {
            conversation.IsDeleted = true;
            conversation.DeletedAt = DateTime.Now;
            // remove avatar, background
            if (!string.IsNullOrEmpty(conversation.Avatar))
            {
                await mCloudStorage.DeleteFileAsync(conversation.Avatar);
                conversation.Avatar = null;
            }
            if (!string.IsNullOrEmpty(conversation.Background))
            {
                await mCloudStorage.DeleteFileAsync(conversation.Background);
                conversation.Background = null;
            }
            await mMessageService.DeleteAll(conversation.Id);
            mContext.Conversations.Update(conversation);
        }

        private async Task<ConversationViewModel> GetConversationVM(int userId, Conversation conversation)
        {
            var latestMessage = await mMessageService.GetLatestMessage(conversation.Id);
            var title = conversation.Title;
            var participants = await GetAllParticipant(conversation.Id);
            var avatar = conversation.Avatar;
            if (latestMessage != null)
            {
                var sender = participants.Where(x => x.User.Id == latestMessage.SenderId).FirstOrDefault();
                var name = "người dùng";
                if (sender != null)
                {
                    name = sender.NickName ?? sender.User.Name.Split(" ").LastOrDefault();
                }
                var content = latestMessage.Content;
                latestMessage.Content = name + ": " + content;
            }
            if (string.IsNullOrEmpty(title))
            {
                int count = participants.Count();
                if (count <= 2)
                {
                    var otherPar = participants.Where(x => x.User.Id != userId).FirstOrDefault();
                    title = otherPar.User.Name;
                }
                else
                {
                    title += $"Bạn và {count - 1} người khác";
                }
            }

            if (string.IsNullOrEmpty(avatar) && participants.Count <= 2)
            {
                var receiver = participants.Where(x => x.User.Id != userId).FirstOrDefault();
                avatar = receiver.User.Avatar;
            }

            var conversationVM = new ConversationViewModel()
            {
                Id = conversation.Id,
                Title = title,
                Description = conversation.Description ?? "",
                ChannelId = conversation.ChannelId,
                CreatorId = conversation.UserId,
                Avatar = avatar ?? "",
                Background = conversation.Background ?? "",
                LatestMessage = latestMessage,
                CreatedAt = conversation.CreatedAt,
                UpdatedAt = conversation.UpdatedAt,
                DeletedAt = conversation.DeletedAt,
            };
            return conversationVM;
        }

        public async Task<List<ParticipantViewModel>> GetAllParticipant(int conversationId)
        {
            var takenData = await (from participant in mContext.Participants
                                   where participant.ConversationId == conversationId && participant.IsJoined
                                   join user in mContext.AppUsers on participant.UserId equals user.Id
                                   where !user.IsDeleted && !user.IsLocked
                                   select new { participant, user }).ToListAsync();
            var participantVMList = (from x in takenData
                                     select ParticipantService.GetParticipantViewModel(x.user, x.participant)).ToList();
            return participantVMList;
        }
    }
}