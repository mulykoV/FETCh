using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace FETCh.Authorization
{
    public class ForumAccessHandler : AuthorizationHandler<ForumAccessRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ForumAccessRequirement requirement)
        {

            if (context.User.Claims.Any(c =>
                (c.Type == "IsMentor" && c.Value == "true") ||
                (c.Type == "IsVerifiedUser" && c.Value == "true") ||
                (c.Type == "HasForumAccess" && c.Value == "true")))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
