using System;

namespace Billing_Api.Core.Models.Entities
{
    public class Order : EntityBase
    {
        public long OrderNumber { get; set; }
        public decimal PayableAmount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }
        
        public long PaymentGatewayId { get; set; }
        public PaymentGateway PaymentGateway { get; set; }
    }
}