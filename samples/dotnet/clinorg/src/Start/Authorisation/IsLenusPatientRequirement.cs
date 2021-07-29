using Lenus.Samples.ClinicianOrg.Config;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Lenus.Samples.ClinicianOrg.Start.Authorisation
{
    public class IsLenusPatientRequirement : RolesAuthorizationRequirement
    {
        public IsLenusPatientRequirement() : base(new[] { RoleNames.Patient })
        {
        }
    }
}
