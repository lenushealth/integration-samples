using System;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public class AgencyOptions
    {
        public string BaseApiUri { get; set; } = string.Empty;
        public string InviteApiPath { get; set; } = "/createagencyinvite";
        public string BrowserRedirectPath { get; set; } = "/Patient/Redirect";
        public string ClientNotifyPath { get; set; } = "/Agency/Complete";

        public Uri GetInviteApiEndpoint() => new Uri($"{BaseApiUri.TrimEnd('/')}/{InviteApiPath.TrimStart('/')}", UriKind.Absolute);
    }
}
