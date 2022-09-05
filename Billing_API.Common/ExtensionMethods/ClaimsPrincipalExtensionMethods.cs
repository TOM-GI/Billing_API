using System;
using System.Linq;
using System.Security.Claims;
using Billing_API.Common.Constans;
using Billing_API.Common.Oauth;
using Microsoft.AspNetCore.Http;

namespace Billing_API.Common.ExtensionMethods
{
    public static class ClaimsPrincipalExtensionMethods
    {
        public static UserJwtClaims ExtractUserClaimsFromPrincipals(this ClaimsPrincipal principal)
        {
            if (principal is null)
            {
                return null;
            }
            
            var userIdClaim = principal.Claims.FirstOrDefault(x => x.Type.Equals(ClaimsConstans.UserId));

            if (userIdClaim is null || !long.TryParse(userIdClaim.Value, out var userIdClaimValue))
            {
                return new UserJwtClaims(1); //We should be using token auth, but it would be too much for this example
                //throw new UnauthorizedAccessException();
            }

            return new UserJwtClaims(userIdClaimValue);
        }
    }
}