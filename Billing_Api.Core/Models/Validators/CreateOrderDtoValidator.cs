using Billing_Api.Core.Models.Dtos;
using FluentValidation;

namespace Billing_Api.Core.Models.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(prop => prop.OrderNumber).NotEmpty();
            RuleFor(prop => prop.PayableAmount).NotEmpty();
            RuleFor(prop => prop.PaymentGateway).NotEmpty();
            RuleFor(prop => prop.UserId).NotEmpty();
            RuleFor(prop => prop.Description).MaximumLength(1000);
        }
    }
}