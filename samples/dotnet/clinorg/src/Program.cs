using Lenus.Samples.ClinicianOrg.Config;
using Lenus.Samples.ClinicianOrg.Services.Config;
using Lenus.Samples.ClinicianOrg.Start.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(o =>
{
    o.Conventions.AuthorizeFolder("/Agency", PolicyNames.CanManageAgency);
    o.Conventions.AllowAnonymousToFolder("/Patient");
});

builder.Services.AddLenusApiServices();
builder.Services.AddLenusAuthentication(builder.Configuration);
builder.Services.AddLenusAuthorisation();

var app = builder.Build();

if (!builder.Environment.IsDevelopment())
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

app.MapGet("Agency/Complete", ctx =>
{
    var logger = ctx.RequestServices.GetService<ILogger<Program>>();
    var state = ctx.Request.Query["state"].ToString();
    var subject = ctx.Request.Query["subject"].ToString();

    logger!.LogInformation("Received agency completion response {subject} - {state}", subject, state);

    ctx.Response.StatusCode = 204;

    return Task.CompletedTask;
}).WithMetadata(new AllowAnonymousAttribute());

app.MapRazorPages();

app.Run();
