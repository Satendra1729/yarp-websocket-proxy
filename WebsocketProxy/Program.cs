var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


var app = builder.Build();

app.UseWebSockets();

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UserRequestInterceptor();
});

app.Run();
