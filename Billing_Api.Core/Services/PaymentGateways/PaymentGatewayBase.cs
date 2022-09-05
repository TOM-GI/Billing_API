using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Responses;

namespace Billing_Api.Core.Services.PaymentGateways
{
    public abstract class PaymentGatewayBase
    {
        public abstract PaymentGatewayProcessResponse Process(CreateOrderDto orderDto);
    }
}