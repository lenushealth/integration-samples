using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MyBp.Client;
using MyBp.Config;
using MyBp.Services;
using Polly;
using Refit;
using System;
using System.Threading.Tasks;

namespace MyBp.Startup
{
    public static class LenusServiceExtensions
    {
        public static IServiceCollection AddLenusAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(o =>
                {
                    o.AccessDeniedPath = "/error";
                    o.LogoutPath = "/Home/Logout";
                    o.LoginPath = "/Home/Login";
                })
                .AddOpenIdConnect(o =>
                {
                    configuration.GetSection("OpenIdConnect").Bind(o);
                    /* save the access token within an authentication cookie */
                    o.SaveTokens = true;
                    /* match token and cookie lifetime */
                    o.UseTokenLifetime = true;

                    o.GetClaimsFromUserInfoEndpoint = true;

                    /* use the hybrid flow */
                    o.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                    o.Events.OnRemoteFailure += ctx =>
                    {
                        ctx.Response.Redirect($"/?error={ctx?.Failure?.Message}");
                        ctx.HandleResponse();
                        return Task.CompletedTask;
                    };

                    /* Mandatory scope */
                    o.Scope.Add("openid");

                    /* I want profile information (givenname, familyname) */
                    o.Scope.Add("profile");

                    /* I want to read email address */
                    o.Scope.Add("email");

                    /* I want to read blood pressure data */
                    o.Scope.Add("read.blood_pressure");
                    o.Scope.Add("read.blood_pressure.blood_pressure_systolic");
                    o.Scope.Add("read.blood_pressure.blood_pressure_diastolic");

                    /* I want to write blood pressure data */
                    o.Scope.Add("write.blood_pressure");
                    o.Scope.Add("write.blood_pressure.blood_pressure_systolic");
                    o.Scope.Add("write.blood_pressure.blood_pressure_diastolic");
                });

            return serviceCollection;
        }

        public static IServiceCollection AddLenusAuthorisation(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddAuthorization(o =>
            {
                o.AddPolicy("Query", policy => policy.RequireAuthenticatedUser());
                o.AddPolicy("Submit", policy => policy.RequireAuthenticatedUser());
            });
        }

        public static IServiceCollection AddLenusHealthClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<IAccessTokenAccessor, AccessTokenAccessor>();
            serviceCollection.AddOptions();

            serviceCollection.AddTransient(typeof(HealthClientV2HttpClientHandler));

            serviceCollection.AddRefitClient<IHealthDataClient>()
                .ConfigureHttpClient(c =>
                {
                    var options = configuration.GetSection("HealthDataClient").Get<HealthDataClientOptions>();

                    c.BaseAddress = options.BaseUri;
                })
                .AddHttpMessageHandler<HealthClientV2HttpClientHandler>()
                .AddTransientHttpErrorPolicy(
                    builder => builder.WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
            );

            return serviceCollection;
        }
    }
}