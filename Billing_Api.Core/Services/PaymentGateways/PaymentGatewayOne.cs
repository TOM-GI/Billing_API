using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Responses;

namespace Billing_Api.Core.Services.PaymentGateways
{
    public class PaymentGatewayOne : PaymentGatewayBase
    {
        public override PaymentGatewayProcessResponse Process(CreateOrderDto orderDto) 
        {
            // Payment logic here
            return new PaymentGatewayProcessResponse() { IsSuccess = true };
        }
    }
}