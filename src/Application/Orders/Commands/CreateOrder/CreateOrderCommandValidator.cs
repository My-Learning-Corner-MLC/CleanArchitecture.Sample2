using FluentValidation;
using Sample2.Application.Constants;

namespace Sample2.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.BuyerId)
            .NotNull()
            .NotEmpty();

        RuleFor(o => o.ShipToAddress)
            .NotNull()
            .NotEmpty();

        RuleFor(o => o.OrderItems)
            .NotNull()
                .WithMessage(OrderConst.ErrorMessages.ORDER_NEED_AT_LEAST_ONE_ITEM)
            .Must(x => x.Any())
                .WithMessage(OrderConst.ErrorMessages.ORDER_NEED_AT_LEAST_ONE_ITEM);

    }
}