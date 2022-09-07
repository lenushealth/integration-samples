using Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models;
using Refit;

namespace Lenus.Samples.ClientCredentialsFlow.Services.Organisations
{
    public interface IOrganisationApiService
    {
        [Post("/getmembership")]
        Task<HttpResponseMessage> SendInvite([Body] AgencyInviteRequest agencyInviteRequest, CancellationToken token);
    }
}
