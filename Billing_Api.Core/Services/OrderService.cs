using System.Threading;
using System.Threading.Tasks;
using Billing_API.Common.Attributes;
using Billing_API.Common.Oauth;
using Billing_Api.Core.Database;
using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Entities;
using Billing_Api.Core.Models.Responses;
using Billing_Api.Core.Services.PaymentGateways;
using LinqKit;

namespace Billing_Api.Core.Services
{
    [InjectService(InjectAs.ImplementingInterface)]
    public class OrderService : IOrderService
    {
        private readonly IDbServiceBase _dbServiceBase;
        private readonly IPermissionService _permissionService;

        public OrderService(IDbServiceBase dbServiceBase, IPermissionService permissionService)
        {
            _dbServiceBase = dbServiceBase;
            _permissionService = permissionService;
        }

        public async Task<BusinessResultResponse<Order>> GetOrderAsync(long orderId, UserJwtClaims userJwtClaims, CancellationToken cancellationToken)
        {
            var fetchedOrder = await GetSingleOrderAsync(orderId, userJwtClaims, cancellationToken);

            return BusinessResultResponse<Order>.SuccessByPayload(fetchedOrder);
        }

        public async Task<BusinessResultResponse<Order>> CreateOrderAsync(CreateOrderDto createOrderDto, UserJwtClaims userJwtClaims, CancellationToken cancellationToken)
        {
            var gatewayProcessor = PaymentGatewayFactory.Factorize(createOrderDto.PaymentGateway);
            var gatewayProcessResponse = gatewayProcessor.Process(createOrderDto);

            if (!gatewayProcessResponse.IsSuccess) return BusinessResultResponse<Order>.Fail(gatewayProcessResponse.Error);
            var entity = createOrderDto.CreateOrderEntity();
            
            await _dbServiceBase.AddAsync(entity, cancellationToken);
            
            return BusinessResultResponse<Order>.Ok(entity);
        }

        public async Task<BusinessResultResponse<Order>> DeleteOrderAsync(long orderId, UserJwtClaims userJwtClaims, CancellationToken cancellationToken)
        {
            var fetchedOrder = await GetSingleOrderAsync(orderId, userJwtClaims, cancellationToken);
            if (fetchedOrder == null) return BusinessResultResponse<Order>.Fail(null);

            fetchedOrder.IsDeleted = true;
            _dbServiceBase.UpdateManyProperty(fetchedOrder, x => x.IsDeleted);
            await _dbServiceBase.SaveChangesAsync(cancellationToken);

            return BusinessResultResponse<Order>.Ok(fetchedOrder);
        }

        private Task<Order> GetSingleOrderAsync(long orderId, UserJwtClaims userJwtClaims, CancellationToken cancellationToken)
        {
            var getOrderExp = PredicateBuilder.New<Order>(x => x.Id == orderId);
            var expWithPerm = _permissionService.BuildExpressionToAccessOrder(getOrderExp, userJwtClaims);
            return _dbServiceBase.GetOneByExpression(expWithPerm, cancellationToken);
        }
    }
}