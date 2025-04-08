using Grpc.Core;

namespace OrderingSystemUsingGRPC.PaymentService.Services
{
    public class PaymentService : PaymentServiceP.PaymentServicePBase
    {
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override Task<DeductUserBalanceResponse> DeductUserBalance(
            DeductUserBalanceRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation($"Payment processed for User: {request.UserId}, Amount: {request.TotalPrice}");

            return Task.FromResult(new DeductUserBalanceResponse
            {
                Success = true,
                Message = $"Payment of ${request.TotalPrice} processed successfully",
                Status = PaymentStatus.PaymentSuccessful
            });
        }
    }
}