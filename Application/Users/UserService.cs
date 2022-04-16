using Application.Common;
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
using ViewModel.Users;

namespace Application.Users
{
    public class UserService : IUserService
    {
        private readonly MessageRoomDbContext mContext;
        private readonly IConfiguration mCongfiguration;

        public UserService(MessageRoomDbContext context, IConfiguration configuration)
        {
            mContext = context;
            mCongfiguration = configuration;
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
            // response data
            var authVM = new AuthenticateViewModel()
            {
                Token = token,
                Data = GetUserViewModel(user)
            };
            return new ApiSuccessResult<AuthenticateViewModel>(authVM);
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