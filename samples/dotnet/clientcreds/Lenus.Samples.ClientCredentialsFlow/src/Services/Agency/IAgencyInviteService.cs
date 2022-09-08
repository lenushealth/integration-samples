using Refit;

namespace Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models
{
    public interface IAgencyInviteService
    {

        [Post("/createagencyinvite")]
        Task<HttpResponseMessage> SendInvite([Body] AgencyInviteRequest agencyInviteRequest, CancellationToken token);
    }
}

