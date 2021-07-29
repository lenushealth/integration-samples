using Lenus.Samples.ClinicianOrg.Services.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Services.Impl
{
    public class BasicAgencyInviteService : IAgencyInviteService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<BasicAgencyInviteService> logger;
        private readonly AgencyOptions agencyOptions;

        public BasicAgencyInviteService(IOptions<AgencyOptions> agencyOptions, IHttpClientFactory httpClientFactory, ILogger<BasicAgencyInviteService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.agencyOptions = agencyOptions.Value;
        }

        public async Task SendInvite(string? emailAddress, string? mobileNumber, IEnumerable<string> scopes, Guid? organisationId, CancellationToken cancellationToken)
        {
            var agencyRequest = new
            {
                email = emailAddress,
                phoneNumber = mobileNumber,
                requestedScopes = scopes.ToArray(),
                organizationId = organisationId,
                clientNotifyPath = agencyOptions.ClientNotifyPath,
                clientNotifyState = Guid.NewGuid().ToString(),
                browserRedirectPath = agencyOptions.BrowserRedirectPath,
                browserRedirectState = Guid.NewGuid().ToString(),
            };

            using var httpClient = httpClientFactory.CreateClient("JwtBearer");
            using var response = await httpClient.PostAsJsonAsync(agencyOptions.GetInviteApiEndpoint(), agencyRequest, cancellationToken);
            
            if(!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                logger.LogError($"Failed ({response.StatusCode}) : {errorResponse}");
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
