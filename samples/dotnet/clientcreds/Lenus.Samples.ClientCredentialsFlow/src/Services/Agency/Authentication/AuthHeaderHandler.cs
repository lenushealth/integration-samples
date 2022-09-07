namespace Lenus.Samples.ClientCredentialsFlow.Services.Agency.Authentication
{
    using IdentityModel.Client;
    using Lenus.Samples.ClientCredentialsFlow.Configuration;
    using Microsoft.Extensions.Options;
    using System.Net.Http.Headers;
    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IOptions<LenusClientOptions> clientOptions;

        public AuthHeaderHandler(IOptions<LenusClientOptions> clientOptions)
        {
            this.clientOptions = clientOptions;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = new HttpClient();
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
