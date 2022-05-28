using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.System.Users;

namespace Application.System.Users
{
    public interface IUserService
    {
        public Task<ApiResult<AuthenticateViewModel>> Create(UserCreateRequest request);

        public Task<ApiResult<UserViewModel>> Get(int id);

        public Task<ApiResult<PagedResult<BaseUserViewModel>>> GetPaging(UserPagingRequest request);

        public Task<ApiResult<bool>> Update(UserUpdateRequest request);

        public Task<ApiResult<bool>> Delete(BaseUserRequest request);

        public Task<ApiResult<AuthenticateViewModel>> Login(AuthenticateRequest request);

        public Task<ApiResult<bool>> ChangePassword(PasswordChangeRequest request);
    }
}