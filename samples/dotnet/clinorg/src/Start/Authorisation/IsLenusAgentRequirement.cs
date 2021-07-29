using Lenus.Samples.ClinicianOrg.Config;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Lenus.Samples.ClinicianOrg.Start.Authorisation
{
    public class IsLenusAgentRequirement : RolesAuthorizationRequirement
    {
        public IsLenusAgentRequirement() : base(new[] { RoleNames.Agent })
        {
        }
    }
}
