using Lenus.Samples.ClinicianOrg.Services.Impl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lenus.Samples.ClinicianOrg.Services.Config
{
    public static class LenusApiServicesExtensions
    {
        public static IServiceCollection AddLenusApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
            services.AddHttpClient("JwtBearer")
                .ConfigureHttpClient(async (sp, c) => {
                    var tokenProvider = sp.GetRequiredService<IAccessTokenProvider>();
                    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await tokenProvider.GetAccessToken());
                });
            services.AddSingleton<IConfigureOptions<AgencyOptions>, ConfigureAgencyOptions>();
            services.AddScoped<IAgencyInviteService, BasicAgencyInviteService>();

            services.AddSingleton<IConfigureOptions<OrganisationsOptions>, ConfigureOrganisationsOptions>();
            services.AddScoped<IOrganisationMembershipService, OrganisationMembershipService>();

            return services;
        }
    }
}
