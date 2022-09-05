using Billing_Api.Core.Models.Entities;

namespace Billing_Api.Core.Models.Dtos
{
    public class CreateOrderDto
    {
        public long OrderNumber { get; set; }//Probably should be GUID
        public long UserId { get; set; } //Probably should be in claims
        public decimal PayableAmount { get; set; }
        public long PaymentGateway { get; set; }
        public string Description { get; set; }

        public Order CreateOrderEntity()
        {
            return new Order()
            {
                Description = Description,
                OrderNumber = OrderNumber,
                PaymentGatewayId = PaymentGateway,
                PayableAmount = PayableAmount
            };
        }
    }
}