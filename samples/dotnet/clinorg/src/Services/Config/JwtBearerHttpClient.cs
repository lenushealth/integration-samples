using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public class JwtBearerHttpClient : HttpClient
    {
        private readonly IAccessTokenProvider accessTokenProvider;

        public JwtBearerHttpClient(IAccessTokenProvider accessTokenProvider)
        {
            this.accessTokenProvider = accessTokenProvider;
        }

        public async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await accessTokenProvider.GetAccessToken(cancellationToken);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
