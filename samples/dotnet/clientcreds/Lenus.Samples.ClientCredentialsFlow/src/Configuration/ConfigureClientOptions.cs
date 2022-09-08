using Microsoft.Extensions.Options;

namespace Lenus.Samples.ClientCredentialsFlow.Configuration
{
    public class ConfigureClientOptions : IConfigureOptions<LenusClientOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureClientOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(LenusClientOptions options)
        {
            configuration.GetSection("Lenus:OpenIdConnect").Bind(options);
        }
    }
}
