using Lenus.Samples.ClinicianOrg.Services.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{
    public class OrganisationMembershipService : IOrganisationMembershipService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly OrganisationsOptions options;

        public OrganisationMembershipService(IHttpClientFactory httpClientFactory, IOptions<OrganisationsOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options.Value;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LenusOrganisationalMembership>> Retrieve(CancellationToken cancellationToken = default)
        {
            var lenusMembershipApiEndpointUri = options.GetApiEndpoint();

            using var client = httpClientFactory.CreateClient("JwtBearer");
            var request = new HttpRequestMessage(HttpMethod.Get, lenusMembershipApiEndpointUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var streamContent = await response.Content.ReadAsStreamAsync();
            var memberships = await JsonSerializer.DeserializeAsync<IEnumerable<LenusOrganisationalMembership>>(streamContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return memberships;
        }
    }
}
