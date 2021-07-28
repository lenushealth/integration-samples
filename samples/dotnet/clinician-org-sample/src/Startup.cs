using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lenus.Samples.ClinicianOrg.Config;
using Lenus.Samples.ClinicianOrg.Services;
using Lenus.Samples.ClinicianOrg.Services.Config;
using Lenus.Samples.ClinicianOrg.Start.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lenus.Samples.ClinicianOrg
{
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
            services.AddRazorPages(o =>
            {
                o.Conventions.AuthorizeFolder("/Agency", PolicyNames.CanManageAgency);
                o.Conventions.AllowAnonymousToFolder("/Patient");
            });

            services.AddAgencyServices();
            services.AddLenusAuthentication(Configuration);
            services.AddLenusAuthorisation();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /* Example of a simple, unauthenticated endpoint able to receive the agency callback, of course this will only work if app is deployed and publicly accessible */
                endpoints.MapGet("Agency/Complete", ctx =>
                {
                    var logger = ctx.RequestServices.GetService<ILogger<Startup>>();
                    var state = ctx.Request.Query["state"].ToString();
                    var subject = ctx.Request.Query["subject"].ToString();

                    logger.LogInformation("Received agency completion response {subject} - {state}", subject, state);

                    ctx.Response.StatusCode = 204;

                    return Task.CompletedTask;
                }).WithMetadata(new AllowAnonymousAttribute());

                endpoints.MapRazorPages();
            });
        }
    }
}
