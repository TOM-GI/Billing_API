using Billing_API.Common.ExtensionMethods;
using Billing_API.Common.Oauth;
using Billing_Api.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Billing_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingBaseController : ControllerBase
    {
        protected UserJwtClaims UserClaims => HttpContext.User.ExtractUserClaimsFromPrincipals();

        protected IActionResult ResponseFromBusinessResult<T>(BusinessResultResponse<T> response) where T : class
        {
            if (response.IsSuccess) return Ok(response.Payload);
            if (response.Payload == null) return NotFound();
            return BadRequest(response.Error);
        }
        
        protected IActionResult ResponseFromBusinessResult(BusinessResultResponse response) 
        {
            if (response.IsSuccess) return Ok();
            return BadRequest(response.Error);
        }
    }
}