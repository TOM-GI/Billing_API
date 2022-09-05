using System;
using Billing_Api.Core.Models.Dtos;

namespace Billing_Api.Core.Services.PaymentGateways
{
    public static class PaymentGatewayFactory
    {
        public static PaymentGatewayBase Factorize(long paymentGatewayId)
        {
            switch (paymentGatewayId)
            {
                case 1:
                    return new PaymentGatewayOne();
                case 2:
                    return new PaymentGatewayTwo();
                default:
                    throw new Exception();
            }
        }
    }
}