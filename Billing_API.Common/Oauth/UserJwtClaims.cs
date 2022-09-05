using System;

namespace Billing_API.Common.Oauth
{
    public class UserJwtClaims
    {
        public UserJwtClaims(long userId) { UserId = userId; }
        public long UserId { get; } //Probably GUID would be better
    }
}