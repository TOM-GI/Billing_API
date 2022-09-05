using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Billing_Api.Core.Database;
using Billing_Api.Core.Models.Dtos;
using Billing_Api.Core.Models.Entities;
using Billing_Api.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace Billing_API.Tests
{
    public class OrderServiceTests // Just one test per method, only for one service, because lack of time
    {
        private IOrderService _target;
        private IPermissionService _permissionService;
        private IDbServiceBase _dbServiceBase;

        [SetUp]
        public void Setup()
        {
            _dbServiceBase = Substitute.For<IDbServiceBase>();
            _permissionService = Substitute.For<IPermissionService>();
            _target = new OrderService(_dbServiceBase, _permissionService);
        }
        
        [Test]
        public async Task GetOrderAsyncTest()
        {
            //Arrange
            var order = new Order() { Id = 333, PayableAmount = 123 };
            _dbServiceBase.GetOneByExpression(Arg.Any<Expression<Func<Order, bool>>>(), Arg.Any<CancellationToken>()).Returns(order);
            //Act
            var call = await _target.GetOrderAsync(order.Id, null, CancellationToken.None);
            //Assert
            Assert.That(call.Payload.PayableAmount == order.PayableAmount);
        }

        [Test]
        public async Task DeleteOrderAsyncTest() 
        {
            //Arrange
            var order = new Order() { Id = 333, PayableAmount = 123 };
            _dbServiceBase.GetOneByExpression(Arg.Any<Expression<Func<Order, bool>>>(), Arg.Any<CancellationToken>()).Returns(order);
            //Act
            var call = await _target.DeleteOrderAsync(order.Id, null, CancellationToken.None);
            //Assert
            Assert.True(call.Payload.IsDeleted);
        }
        
        [Test]
        public async Task CreateOrderAsyncTest() 
        {
            //Arrange
            var createOrderDto = new CreateOrderDto() { PayableAmount = 123, PaymentGateway = 1, OrderNumber = 123};
            //Act
            var call = await _target.CreateOrderAsync(createOrderDto, null, CancellationToken.None);
            //Assert
            Assert.True(call.Payload.OrderNumber == createOrderDto.OrderNumber);
        }
    }
}