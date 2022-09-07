namespace Lenus.Samples.ClientCredentialsFlow.Configuration
{
    using Microsoft.Extensions.Options;
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
