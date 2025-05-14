using Amazon.Runtime;
using PurplePete.Components;
using PurplePete.ConfluenceProvider;
using PurplePete.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddConfluenceProvider(builder.Configuration);

builder.Services.AddSingleton<AccountInfo>();
builder.Services.AddSingleton<BedrockChatAI>();
builder.Services.AddSingleton<ConfluenceService>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<TitanAI>(provider =>
{
    var credentials = new BasicAWSCredentials("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY");

    return new TitanAI(credentials);
});

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
