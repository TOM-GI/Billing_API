using System.Threading;
using System.Threading.Tasks;
using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billing_API.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class OrderController : BillingBaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) { _orderService = orderService; }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(long orderId, CancellationToken cancellationToken = default)
        {
            var businessResultResponse = await _orderService.GetOrderAsync(orderId, UserClaims, cancellationToken);
            return ResponseFromBusinessResult(businessResultResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateOrderDto orderDto, CancellationToken cancellationToken = default)
        {
            var createResult = await _orderService.CreateOrderAsync(orderDto, UserClaims, cancellationToken);
            return ResponseFromBusinessResult(createResult);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(long orderId, CancellationToken cancellationToken = default)
        {
            var businessResultResponse = await _orderService.DeleteOrderAsync(orderId, UserClaims, cancellationToken);
            return ResponseFromBusinessResult(businessResultResponse);
        }
    }
}