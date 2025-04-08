using Grpc.Core;

namespace OrderingSystemUsingGRPC.InventoryService.Services
{
    public class InventoryService : InventoryServiceP.InventoryServicePBase
    {
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(ILogger<InventoryService> logger)
        {
            _logger = logger;
        }

        public override Task<DeductItemsResponse> DeductItemsBalance(
            DeductItemsRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation($"Inventory updated for Order: {request.OrderId}");

            foreach (var item in request.Items)
            {
                _logger.LogInformation($"Item: {item.ItemId}, Quantity: {item.Quantity}");
            }

            return Task.FromResult(new DeductItemsResponse
            {
                Success = true,
                Message = "Inventory updated successfully",
                Status = InventoryStatus.InventoryAvailable
            });
        }
    }
}
