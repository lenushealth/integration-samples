namespace Lenus.Samples.ClientCredentialsFlow.Configuration
{
    using Microsoft.Extensions.Options;
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
