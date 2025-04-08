using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingSystemUsingGRPC.OrderingService;

namespace OrderingSystemUsingGRPC.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderingServiceP.OrderingServicePClient _orderingClient;

        public OrdersController(OrderingServiceP.OrderingServicePClient orderingClient)
        {
            _orderingClient = orderingClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var orderRequest = new OrderRequest
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = orderDto.UserId
            };

            orderRequest.Items.AddRange(orderDto.Items.Select(item => new OrderItem
            {
                ItemId = item.ItemId,
                Price = item.Price,
                Quantity = item.Quantity
            }));

            var response = await _orderingClient.SubmitOrderAsync(orderRequest);
            return Ok(response);
        }
    }

    public class OrderDto
    {
        public string UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }

    public class OrderItemDto
    {
        public string ItemId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
