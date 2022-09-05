using System;
using System.Linq.Expressions;
using Billing_API.Common.Attributes;
using Billing_API.Common.Oauth;
using Billing_Api.Core.Models.Entities;
using LinqKit;

namespace Billing_Api.Core.Services
{
    public interface IPermissionService
    {
        public Expression<Func<Order, bool>> BuildExpressionToAccessOrder(Expression<Func<Order, bool>> expression, UserJwtClaims userClaims);
    }

    [InjectService(InjectAs.ImplementingInterface)]
    public class PermissionService : IPermissionService
    {
        public Expression<Func<Order, bool>> BuildExpressionToAccessOrder(Expression<Func<Order, bool>> expression, UserJwtClaims userClaims) 
        {
            //Here we use LinqKit to assembly expression only to fetch orders we have access to 

            Expression<Func<Order, bool>> resultExpression = PredicateBuilder.New<Order>(x => true);
            //resultExpression = expression.And(order => order.UserId == userClaims.UserId); // Only my orders
            return resultExpression;
        }
    }
}