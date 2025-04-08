using Grpc.Core;
using OrderingSystemUsingGRPC.InventoryService;
using OrderingSystemUsingGRPC.PaymentService;

namespace OrderingSystemUsingGRPC.OrderingService.Services
{
    public class OrderingService : OrderingServiceP.OrderingServicePBase
    {
        private readonly ILogger<OrderingService> _logger;
        private readonly PaymentServiceP.PaymentServicePClient _paymentClient;
        private readonly InventoryServiceP.InventoryServicePClient _inventoryClient;

        public OrderingService(
            ILogger<OrderingService> logger,
            PaymentServiceP.PaymentServicePClient paymentClient,
            InventoryServiceP.InventoryServicePClient inventoryClient)
        {
            _logger = logger;
            _paymentClient = paymentClient;
            _inventoryClient = inventoryClient;
        }

        public override async Task<OrderResponse> SubmitOrder(
            OrderRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation($"New order received - OrderId: {request.OrderId}, UserId: {request.UserId}");

            var inventoryRequest = new DeductItemsRequest
            {
                OrderId = request.OrderId
            };
            inventoryRequest.Items.AddRange(request.Items.Select(item =>
                new InventoryService.InventoryItem
                {
                    ItemId = item.ItemId,
                    Quantity = item.Quantity
                }));

            var inventoryResponse = await _inventoryClient.DeductItemsBalanceAsync(inventoryRequest);
            _logger.LogInformation($"Inventory Service Response: {inventoryResponse.Message}");

            var totalAmount = request.Items.Sum(item => item.Price * item.Quantity);
            var paymentRequest = new DeductUserBalanceRequest
            {
                UserId = request.UserId,
                OrderId = request.OrderId,
                TotalPrice = totalAmount
            };

            var paymentResponse = await _paymentClient.DeductUserBalanceAsync(paymentRequest);
            _logger.LogInformation($"Payment Service Response: {paymentResponse.Message}");

            return new OrderResponse
            {
                Success = true,
                Message = "Order processed successfully",
                OrderId = request.OrderId,
                Status = OrderStatus.OrderCompleted
            };
        }

        public override Task<OrderStatusResponse> GetOrderStatus(
            OrderStatusRequest request,
            ServerCallContext context)
        {
            return Task.FromResult(new OrderStatusResponse
            {
                OrderId = request.OrderId,
                Status = OrderStatus.OrderCompleted,
                Message = "Order is completed"
            });
        }
    }
}
