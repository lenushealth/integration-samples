using Lenus.Samples.ClinicianOrg.Services.Config;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services
{
    public class LenusOrganisationalMembership
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public interface IOrganisationMembershipService
    {
        Task<IEnumerable<LenusOrganisationalMembership>> Retrieve(CancellationToken cancellationToken = default);
    }

    public class OrganisationMembershipService : IOrganisationMembershipService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly OrganisationsOptions options;
        private readonly ILogger<OrganisationMembershipService> logger;

        public OrganisationMembershipService(IHttpClientFactory httpClientFactory, IOptions<OrganisationsOptions> options, ILogger<OrganisationMembershipService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.options = options.Value;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<LenusOrganisationalMembership>> Retrieve(CancellationToken cancellationToken = default)
        {
            var lenusMembershipApiEndpointUri = options.GetApiEndpoint();

            using var client = httpClientFactory.CreateClient("JwtBearer");
            var request = new HttpRequestMessage(HttpMethod.Get, lenusMembershipApiEndpointUri);

            logger.LogInformation("Requesting Lenus organisation memberships");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            logger.LogDebug("Reading response body stream");
            var streamContent = await response.Content.ReadAsStreamAsync();
            logger.LogDebug("Parsing response from membership API");
            var memberships = await JsonSerializer.DeserializeAsync<IEnumerable<LenusOrganisationalMembership>>(streamContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return memberships;
        }
    }
}
