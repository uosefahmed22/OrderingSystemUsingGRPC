using OrderingSystemUsingGRPC.InventoryService;
using OrderingSystemUsingGRPC.OrderingService;
using OrderingSystemUsingGRPC.OrderingService.Services;
using OrderingSystemUsingGRPC.PaymentService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<OrderingServiceP.OrderingServicePClient>(options =>
{
    options.Address = new Uri("https://localhost:5138");
});

builder.Services.AddGrpcClient<InventoryServiceP.InventoryServicePClient>(options =>
{
    options.Address = new Uri("https://localhost:5285");
});

builder.Services.AddGrpcClient<PaymentServiceP.PaymentServicePClient>(options =>
{
    options.Address = new Uri("https://localhost:5229");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
