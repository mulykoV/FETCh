using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace FETCh.Authorization
{
    public class MinimumWorkingHoursHandler : AuthorizationHandler<MinimumWorkingHoursRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumWorkingHoursRequirement requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(c => c.Type == "WorkingHours");

            if (claim != null && int.TryParse(claim.Value, out int hours))
            {
                if (hours >= requirement.MinimumHours)
                {
                    context.Succeed(requirement);
                }
            }
                
            return Task.CompletedTask;
        }
    }
}
