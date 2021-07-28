using System;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public class OrganisationsOptions
    {
        public string BaseApiUri { get; set; } = string.Empty;
        public string MembershipApiPath { get; set; } = "/membership";

        public Uri GetApiEndpoint() => new Uri($"{BaseApiUri.TrimEnd('/')}/{MembershipApiPath.TrimStart('/')}", UriKind.Absolute);
    }
}
