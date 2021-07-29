using System;
using Polly;

namespace MyBp
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Client;
    using Config;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Refit;
    using Services;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Microsoft.Extensions.Hosting;

    public static class LenusAuthenticationExtensions 
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
                })
                ;

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

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddMvc();

            services.AddLenusAuthentication(Configuration);
            services.AddLenusAuthorisation();
            services.AddLenusHealthClient(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            var gbCulture = CultureInfo.GetCultureInfo("en-GB");
            app.UseRequestLocalization(
                new RequestLocalizationOptions()
                {
                    DefaultRequestCulture = new RequestCulture(gbCulture),
                    SupportedCultures = new[] { gbCulture },
                    SupportedUICultures = new[] { gbCulture }
                });

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapDefaultControllerRoute();
            });
        }
    }
}
