using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public class ConfigureOrganisationsOptions : IConfigureOptions<OrganisationsOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureOrganisationsOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(OrganisationsOptions options)
        {
            configuration.GetSection("Lenus:Organisations").Bind(options);
        }
    }
}
