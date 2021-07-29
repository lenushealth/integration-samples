using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public class ConfigureAgencyOptions : IConfigureOptions<AgencyOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureAgencyOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(AgencyOptions options)
        {
            configuration.GetSection("Lenus:Agency").Bind(options);
        }
    }
}
