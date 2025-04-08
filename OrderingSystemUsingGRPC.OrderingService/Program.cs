using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrderingSystemUsingGRPC.InventoryService;
using OrderingSystemUsingGRPC.OrderingService.Services;
using OrderingSystemUsingGRPC.PaymentService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddGrpcClient<InventoryServiceP.InventoryServicePClient>(options =>
{
    options.Address = new Uri("https://localhost:5285");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    return handler;
});

builder.Services.AddGrpcClient<PaymentServiceP.PaymentServicePClient>(options =>
{
    options.Address = new Uri("https://localhost:5229");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    return handler;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5138, o =>
    {
        o.Protocols = HttpProtocols.Http2;
        o.UseHttps();
    });
});

var app = builder.Build();

app.MapGrpcService<OrderingService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();