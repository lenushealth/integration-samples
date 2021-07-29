using Lenus.Samples.ClinicianOrg.Config;
using Lenus.Samples.ClinicianOrg.Start.Authorisation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lenus.Samples.ClinicianOrg.Start.Authentication
{
    public static class LenusAuthenticationExtensions
    {
        public static IServiceCollection AddLenusAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            _ = serviceCollection
                .AddScoped<IClaimsTransformation, IdentityClaimsTransformation>()
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(o =>
                {
                    o.AccessDeniedPath = "/error";
                })
                .AddOpenIdConnect(o =>
                {
                    configuration.GetSection("Lenus:OpenIdConnect").Bind(o);
                    /* save the access token within an authentication cookie */
                    o.SaveTokens = true;
                    /* match token and cookie lifetime */
                    o.UseTokenLifetime = true;

                    o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                    o.GetClaimsFromUserInfoEndpoint = true;

                    /* use the hybrid flow */
                    o.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                    o.Events.OnRemoteFailure += ctx =>
                    {
                        ctx.Response.Redirect($"/Error?message={ctx?.Failure?.Message}");
                        ctx?.HandleResponse();
                        return Task.CompletedTask;
                    };

                    /* Mandatory scope */
                    o.Scope.Add("openid");

                    /* I want profile information (givenname, familyname) */
                    o.Scope.Add("profile");

                    /* I want to access APIs associated with consent and agency over another user's data */
                    o.Scope.Add("agency_api");

                    /* Include roles in my claims */
                    o.ClaimActions.Add(new JsonKeyClaimAction("role", ClaimValueTypes.String, "role"));
                    o.ClaimActions.MapJsonKey(JwtRegisteredClaimNames.Sub, JwtRegisteredClaimNames.Sub);
                    o.ClaimActions.DeleteClaim(ClaimTypes.NameIdentifier);

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "role",
                        NameClaimType = "name",
                        RequireSignedTokens = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    };
                });

            return serviceCollection;
        }

        public static IServiceCollection AddLenusAuthorisation(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddAuthorization(o =>
            {
                o.AddPolicy(PolicyNames.CanManageAgency, policy => policy.AddRequirements(new IsLenusAgentRequirement()));
            });
        }
    }
}
