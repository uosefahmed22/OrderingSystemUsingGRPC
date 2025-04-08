
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrderingSystemUsingGRPC.PaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5229, o =>
    {
        o.Protocols = HttpProtocols.Http2;
        o.UseHttps();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
