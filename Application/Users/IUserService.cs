using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Users;

namespace Application.Users
{
    public interface IUserService
    {
        public Task<ApiResult<AuthenticateViewModel>> Create(UserCreateRequest request);

        public Task<ApiResult<AuthenticateViewModel>> Login(AuthenticateRequest request);
    }
}