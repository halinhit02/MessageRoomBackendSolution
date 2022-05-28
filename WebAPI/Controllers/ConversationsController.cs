using Application.Catalog.Conversations;
using Application.Catalog.Messages;
using Application.Catalog.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using ViewModel.Catalog.Conversations;
using ViewModel.Catalog.Messages;
using ViewModel.Catalog.Participants;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService mConversationService;
        private readonly IMessageService mMessageService;
        private readonly IParticipantService mParticipantService;
        private readonly IAuthorizationService authorizationService;

        public ConversationsController(IConversationService conversationService,
            IMessageService messageService, IParticipantService participantService,
            IAuthorizationService iAuthorizationService)
        {
            mConversationService = conversationService;
            mMessageService = messageService;
            mParticipantService = participantService;
            authorizationService = iAuthorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging([FromQuery] ConversationPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mConversationService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery] ConversationGetRequest request, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mConversationService.GetById(request, id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConversationCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserIds[0].ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mConversationService.Create(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ConversationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mConversationService.Update(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ConversationDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mConversationService.Delete(request);
            return Ok(result);
        }

        //Messages
        //
        [HttpGet("messages")]
        public async Task<IActionResult> GetAllPaging([FromQuery] MessagePagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mMessageService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetAllPaging([FromQuery] BaseMessageRequest request, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mMessageService.GetById(request, id);
            return Ok(result);
        }

        [HttpPost("messages")]
        public async Task<IActionResult> Create([FromBody] MessageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mMessageService.Create(request);
            return Ok(result);
        }

        [HttpDelete("messages")]
        public async Task<IActionResult> Delete([FromQuery] MessageDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mMessageService.Delete(request);
            return Ok(result);
        }

        //Participants
        [HttpGet("participants")]
        public async Task<IActionResult> GetAllPaging([FromQuery] ParticipantPagingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mParticipantService.GetAllPaging(request);
            return Ok(result);
        }

        [HttpPost("participants")]
        public async Task<IActionResult> CreateParticipant([FromBody] ParticipantCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mParticipantService.Create(request);
            return Ok(result);
        }

        [HttpDelete("participants")]
        public async Task<IActionResult> Delete([FromQuery] ParticipantDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mParticipantService.Delete(request);
            return Ok(result);
        }

        [HttpPut("participants")]
        public async Task<IActionResult> UpdateParticipant([FromBody] ParticipantUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User,
                request.UserId.ToString(), PolicyNameConstants.UserIdAuthorization);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            var result = await mParticipantService.Update(request);
            return Ok(result);
        }
    }
}