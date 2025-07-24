using CatFacts.Components;
using CatFacts.Services.Api;
using CatFacts.Services.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var appConfiguration = new ConfigurationBuilder()
  .SetBasePath(AppContext.BaseDirectory)
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .Build();

builder.Services.AddSingleton<IConfigurationRoot>(appConfiguration);

builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(appConfiguration["BackendApi:BaseUrl"]);
});

builder.Services.AddScoped<ICatFactApiService, CatFactApiService>();
builder.Services.AddScoped<IInformationLogger, InformationLogger>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
