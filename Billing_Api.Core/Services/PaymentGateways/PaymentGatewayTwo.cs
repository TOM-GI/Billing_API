using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Responses;

namespace Billing_Api.Core.Services.PaymentGateways
{
    public class PaymentGatewayTwo : PaymentGatewayBase
    {
        public override PaymentGatewayProcessResponse Process(CreateOrderDto orderDto) 
        {
            return new PaymentGatewayProcessResponse() { IsSuccess = true };
        }
    }
}