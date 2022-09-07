namespace Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models
{
    using Refit;

    public interface IAgencyInviteService
    {

        [Post("/createagencyinvite")]
        Task<HttpResponseMessage> SendInvite([Body] AgencyInviteRequest agencyInviteRequest, CancellationToken token);
    }
}

