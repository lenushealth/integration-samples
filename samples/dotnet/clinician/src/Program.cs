using Clinician;
using Clinician.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;


Activity.DefaultIdFormat = ActivityIdFormat.W3C;
Activity.ForceDefaultIdFormat = true;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(k => k.AddServerHeader = false);

builder.Services.AddLocalization();
builder.Services.AddMvc().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.Configure<RouteOptions>(r =>
{
    r.LowercaseUrls = true;
    r.ConstraintMap.Add("sampletype", typeof(SampleDataTypeRouteConstraint));
});

builder.Services.AddLenusAuthentication(builder.Configuration);
builder.Services.AddLenusAuthorisation();
builder.Services.AddLenusHealthClient(builder.Configuration);
builder.Services.AddAgencyServices(builder.Configuration);

builder.Services.AddMemoryCache();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseAuthentication();
app.UseRequestLocalization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseStaticFiles();

var gbCulture = CultureInfo.GetCultureInfo("en-GB");
app.UseRequestLocalization(
    new RequestLocalizationOptions()
    {
        DefaultRequestCulture = new RequestCulture(gbCulture),
        SupportedCultures = new[] { gbCulture },
        SupportedUICultures = new[] { gbCulture }
    });

app.UseRouting();
app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
