using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBp.Startup;
using System.Diagnostics;
using System.Globalization;


Activity.DefaultIdFormat = ActivityIdFormat.W3C;
Activity.ForceDefaultIdFormat = true;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(k => k.AddServerHeader = false);

builder.Services.AddLocalization();
builder.Services.AddMvc();

builder.Services.AddLenusAuthentication(builder.Configuration);
builder.Services.AddLenusAuthorisation();
builder.Services.AddLenusHealthClient(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
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

app.MapDefaultControllerRoute();

app.Run();
