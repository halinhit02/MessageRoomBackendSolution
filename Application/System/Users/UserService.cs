using Application.Catalog.Participants;
using Application.Common;
using Application.System.CloudStorage;
using Application.System.Users;
using MessageRoomSolution.Data.EF;
using MessageRoomSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Common;
using ViewModel.System.Users;

namespace Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly IConfiguration mCongfiguration;
        private readonly ICloudStorage mCloudStorage;

        public UserService(MessageRoomDbContext context, IConfiguration configuration, ICloudStorage cloudStorage)
        {
            mContext = context;
            mCongfiguration = configuration;
            mCloudStorage = cloudStorage;
        }

        public async Task<ApiResult<AuthenticateViewModel>> Create(UserCreateRequest request)
        {
            if ((string.IsNullOrEmpty(request.Email) && string.IsNullOrEmpty(request.Phone))
                || (!CommonUtils.IsValidEmail(request.Email) && !CommonUtils.IsValidPhone(request.Phone)))
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.NotValidPhoneEmail);
            }
            if (!CommonUtils.IsValidPassword(request.Password))
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.NotValidPassword);
            }
            var user = await mContext.AppUsers
                .Where(x => x.Phone == request.Phone || (x.Email == request.Email && !string.IsNullOrEmpty(request.Email)))
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.IsUsedPhoneEmail);
            }
            var newUser = new AppUser()
            {
                Name = request.Name,
                AboutMe = request.AboutMe,
                Dob = request.Dob,
                Email = request.Email ?? "",
                DivisionId = request.DivisionId,
                Avatar = null,
                BranchId = request.BranchId,
                Gender = request.Gender,
                DepartmentId = request.DepartmentId,
                IsAdmin = false,
                IsDeleted = false,
                IsLocked = false,
                Password = CommonUtils.GeneratePassword(request.Password),
                Phone = request.Phone ?? "",
                StaffId = request.StaffId,
                CreatorId = request.CreatorId,
                Token = null,
                CreatedAt = DateTime.Now,
                LastAccess = DateTime.Now,
                TokenOn = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await mContext.AppUsers.AddAsync(newUser);
            var IsSaved = await mContext.SaveChangesAsync() > 0;
            if (!IsSaved)
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.CommonError);
            }
            var authReuqest = new AuthenticateRequest()
            {
                Username = newUser.Phone != "" ? newUser.Phone : newUser.Email,
                Password = request.Password
            };
            return await Login(authReuqest);
        }

        public async Task<ApiResult<UserViewModel>> Get(int Id)
        {
            var user = await mContext.AppUsers.Where(x => x.Id == Id && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>(ResultConstants.IsNotExistedUser);
            }
            else if (user.IsLocked)
            {
                return new ApiErrorResult<UserViewModel>(ResultConstants.IsLockedUser);
            }
            return new ApiSuccessResult<UserViewModel>(GetUserViewModel(user));
        }

        public async Task<ApiResult<PagedResult<BaseUserViewModel>>> GetPaging(UserPagingRequest request)
        {
            var query = await mContext.AppUsers.Where(x => !x.IsDeleted && !x.IsLocked).ToListAsync();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(request.Keyword)
                || x.Phone.ToLower().Contains(request.Keyword)
                || x.Email.ToLower().Contains(request.Keyword)).ToList();
            }
            int totalRow = query.Count();
            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => ParticipantService.GetBaseUserViewModel(x))
                .ToList();
            var pageResult = new PagedResult<BaseUserViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<BaseUserViewModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> Update(UserUpdateRequest request)
        {
            var user = await mContext.AppUsers.Where(x => x.Id == request.Id && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.IsNotExistedUser);
            }
            else if (user.IsLocked)
            {
                return new ApiErrorResult<bool>(ResultConstants.IsLockedUser);
            }
            if (!CommonUtils.VerifyPassword(user, request.Password))
            {
                return new ApiErrorResult<bool>(ResultConstants.NotValidPassword);
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                if (!CommonUtils.IsValidEmail(request.Email))
                {
                    return new ApiErrorResult<bool>(ResultConstants.NotValidEmail);
                }
                user.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Phone) && request.Phone != user.Phone)
            {
                if (!CommonUtils.IsValidPhone(request.Phone))
                {
                    return new ApiErrorResult<bool>(ResultConstants.NotValidPhone);
                }
                user.Phone = request.Phone;
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                user.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.AboutMe))
            {
                user.AboutMe = request.AboutMe;
            }
            if (request.Dob != null && request.Dob.Year != 1)
            {
                user.Dob = request.Dob;
            }
            if (!string.IsNullOrEmpty(request.Gender))
            {
                user.Gender = request.Gender;
            }
            if (!string.IsNullOrEmpty(request.Avatar))
            {
                if (user.Avatar != null)
                {
                    await mCloudStorage.DeleteFileAsync(user.Avatar);
                }
                user.Avatar = request.Avatar;
            }
            user.UpdatedAt = DateTime.Now;
            mContext.AppUsers.Update(user);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task<ApiResult<bool>> Delete(BaseUserRequest request)
        {
            var user = await mContext.AppUsers.Where(x => x.Id == request.Id && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new ApiErrorResult<bool>(ResultConstants.IsNotExistedUser);
            }
            else if (user.IsLocked)
            {
                return new ApiErrorResult<bool>(ResultConstants.IsLockedUser);
            }
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;
            if (user.Avatar != null)
            {
                await mCloudStorage.DeleteFileAsync(user.Avatar);
                user.Avatar = null;
            }
            mContext.AppUsers.Update(user);
            return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
        }

        public async Task<ApiResult<AuthenticateViewModel>> Login(AuthenticateRequest request)
        {
            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username))
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.NotValidLogin);
            }
            var user = await mContext.AppUsers
                .Where(x => x.Email == request.Username || x.Phone == request.Username)
                .FirstOrDefaultAsync();
            if (!CommonUtils.VerifyPassword(user, request.Password))
            {
                return new ApiErrorResult<AuthenticateViewModel>(ResultConstants.NotCorrectLogin);
            }
            //token
            var token = CommonUtils.GenerateToken(user, mCongfiguration);
            // Update Database
            user.TokenOn = DateTime.Now;
            await UpdateLastAccess(user);
            // response data
            var authVM = new AuthenticateViewModel()
            {
                Token = token,
                Data = GetUserViewModel(user)
            };
            return new ApiSuccessResult<AuthenticateViewModel>(authVM);
        }

        public async Task<ApiResult<bool>> ChangePassword(PasswordChangeRequest request)
        {
            if (!CommonUtils.IsValidPassword(request.Password) || string.IsNullOrEmpty(request.Username)
                || !CommonUtils.IsValidPassword(request.NewPassword))
            {
                return new ApiErrorResult<bool>(ResultConstants.NotValidLogin);
            }
            var user = await mContext.AppUsers
                .Where(x => x.Email == request.Username || x.Phone == request.Username)
                .FirstOrDefaultAsync();
            if (CommonUtils.VerifyPassword(user, request.Password))
            {
                var newPassword = CommonUtils.GeneratePassword(request.NewPassword);
                user.Password = newPassword;
                mContext.AppUsers.Update(user);
                return ApiResult<bool>.From(await mContext.SaveChangesAsync() > 0);
            }
            return new ApiErrorResult<bool>(ResultConstants.NotCorrectLogin);
        }

        private async Task<bool> UpdateLastAccess(AppUser user)
        {
            user.LastAccess = DateTime.Now;
            mContext.AppUsers.Update(user);
            return await mContext.SaveChangesAsync() > 0;
        }

        private UserViewModel GetUserViewModel(AppUser user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                AboutMe = user.AboutMe,
                Dob = user.Dob,
                Email = user.Email,
                DivisionId = user.DivisionId,
                Avatar = user.Avatar,
                BranchId = user.BranchId,
                Gender = user.Gender,
                DepartmentId = user.DepartmentId,
                IsAdmin = user.IsAdmin,
                Phone = user.Phone,
                StaffId = user.StaffId,
                CreatorId = user.CreatorId,
                LastAccess = user.LastAccess,
            };
        }
    }
}