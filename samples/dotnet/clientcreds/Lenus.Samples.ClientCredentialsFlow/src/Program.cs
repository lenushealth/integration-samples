using Lenus.Samples.ClientCredentialsFlow.Services.Agency.Models;
using Lenus.Samples.ClientCredentialsFlow.Services.Agency.Authentication;
using Refit;
using Lenus.Samples.ClientCredentialsFlow.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IConfigureOptions<AgencyOptions>, ConfigureAgencyOptions>();
builder.Services.AddSingleton<IConfigureOptions<LenusClientOptions>, ConfigureClientOptions>();

builder.Services.AddTransient<AuthHeaderHandler, AuthHeaderHandler>();

builder.Services.AddHttpClient("AuthTokenProvider");

builder.Services.AddRefitClient<IAgencyInviteService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Lenus:Agency:BaseApiUri"]!))
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
