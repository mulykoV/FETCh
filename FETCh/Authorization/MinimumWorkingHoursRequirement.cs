using Microsoft.AspNetCore.Authorization;

namespace FETCh.Authorization
{
    public class MinimumWorkingHoursRequirement : IAuthorizationRequirement
    {
        public int MinimumHours { get; }

        public MinimumWorkingHoursRequirement(int minimumHours)
        {
            MinimumHours = minimumHours;
        }
    }
}
