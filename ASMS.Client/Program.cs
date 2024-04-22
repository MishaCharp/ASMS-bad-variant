using ASMS.Client.Components;
using ASMS.Client.Services;
using ASMS.Client.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
builder.Services.AddHttpClient<IAuthorizationService, AuthorizationService>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
