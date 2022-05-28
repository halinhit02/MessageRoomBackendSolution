using Application.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModel.System.Users;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthorizationService authorizationService;

        public UsersController(IUserService service, IAuthorizationService iAuthorizationService)
        {
            userService = service;
            authorizationService = iAuthorizationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var result = await userService.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPaging([FromQuery] UserPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var result = await userService.GetPaging(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var result = await userService.Create(request);
            return Ok(result);
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var result = await userService.Login(request);
            return Ok(result);
        }

        [HttpPut("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var result = await userService.ChangePassword(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, request.Id.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await userService.Update(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] BaseUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, request.Id.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await userService.Delete(request);
            return Ok(result);
        }
    }
}