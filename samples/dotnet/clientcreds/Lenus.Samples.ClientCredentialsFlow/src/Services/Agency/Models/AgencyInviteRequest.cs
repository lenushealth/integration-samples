namespace Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models
{
    public class AgencyInviteRequest
    {
        public AgencyInviteRequest(string email, string phoneNumber, IEnumerable<string> requestedScopes, string organizationId, string clientNotifyPath, string browserRedirectPath)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            RequestedScopes = requestedScopes;
            OrganizationId = organizationId;
            ClientNotifyPath = clientNotifyPath;
            BrowserRedirectPath = browserRedirectPath;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> RequestedScopes { get; set; }
        public string OrganizationId { get; set; }
        public string ClientNotifyPath { get; set; }
        public string ClientNotifyState { get { return Guid.NewGuid().ToString(); } }
        public string BrowserRedirectPath { get; set; }
        public string BrowserRedirectState { get { return Guid.NewGuid().ToString(); } }
    }
}
