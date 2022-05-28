using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class UserAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameAuthorRequirement requirement,
                                                       string userId)
        {
            if (context.User == null || requirement == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement
    { }
}