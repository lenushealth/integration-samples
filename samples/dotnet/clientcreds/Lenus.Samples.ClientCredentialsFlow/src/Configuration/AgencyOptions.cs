namespace Lenus.Samples.ClientCredentialsFlow.Configuration
{
    public class AgencyOptions
    {
        public string BaseApiUri { get; set; } = string.Empty;
        public string InviteApiPath { get; set; } = "/createagencyinvite";
        public string BrowserRedirectPath { get; set; } = "/Patient/Redirect";
        public string ClientNotifyPath { get; set; } = "/Agency/Complete";
        public string OrganisationId { get; set; } = string.Empty;
    }
}
