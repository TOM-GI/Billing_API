using System.Threading;
using System.Threading.Tasks;
using Billing_API.Common.Oauth;
using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Entities;
using Billing_Api.Core.Models.Responses;

namespace Billing_Api.Core.Services
{
    public interface IOrderService
    {
        public Task<BusinessResultResponse<Order>> CreateOrderAsync(CreateOrderDto createOrderDto, UserJwtClaims userJwtClaims, CancellationToken cancellationToken);
        public Task<BusinessResultResponse<Order>> GetOrderAsync(long orderId, UserJwtClaims userJwtClaims, CancellationToken cancellationToken);
        public Task<BusinessResultResponse<Order>> DeleteOrderAsync(long orderId, UserJwtClaims userJwtClaims, CancellationToken cancellationToken);
    }
}