namespace Billing_Api.Core.Models.Responses
{
    public class PaymentGatewayProcessResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }
}