using IdentityModel.Client;
using Lenus.Samples.ClientCredentialsFlow.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Lenus.Samples.ClientCredentialsFlow.Services.Agency.Authentication
{
    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IOptions<LenusClientOptions> clientOptions;
        private readonly IHttpClientFactory httpClientFactory;

        public AuthHeaderHandler(IOptions<LenusClientOptions> clientOptions, IHttpClientFactory httpClientFactory)
        {
            this.clientOptions = clientOptions;
            this.httpClientFactory = httpClientFactory;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = httpClientFactory.CreateClient("AuthTokenProvider");

            var token = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = clientOptions.Value.TokenUrl,

                ClientId = clientOptions.Value.ClientId,
                ClientSecret = clientOptions.Value.ClientSecret,
                Scope = "agency_api"
            });

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
