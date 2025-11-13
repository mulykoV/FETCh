using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FETChModels.Models;
using System.Threading.Tasks;

namespace FETCh.Authorization
{
    public class IsCourseAuthorHandler : AuthorizationHandler<IsCourseAuthorRequirement, Course>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IsCourseAuthorHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsCourseAuthorRequirement requirement,
            Course resource)
        {
            if (context.User == null || resource == null) return Task.CompletedTask;

            var userId = _userManager.GetUserId(context.User);

            if (resource.AuthorId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
