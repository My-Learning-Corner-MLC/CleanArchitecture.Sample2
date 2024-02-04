using FluentValidation;
using Sample2.Application.Common.Constants;

namespace Sample2.Application.Orders.Commands.CreateOrder;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.BuyerId)
            .NotNull()
            .NotEmpty();

        RuleFor(p => p.ShipToAddress)
            .NotNull()
            .NotEmpty();

        RuleFor(p => p.OrderItems)
            .NotNull()
                .WithMessage(OrderConst.ErrorMessages.ORDER_NEED_AT_LEAST_ONE_ITEM)
            .Must(x => x.Any())
                .WithMessage(OrderConst.ErrorMessages.ORDER_NEED_AT_LEAST_ONE_ITEM);

    }
}