using FluentValidation;

namespace Sample2.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(o => o.Status)
            .NotNull()
            .NotEmpty();

        RuleFor(o => o.ShipToAddress)
            .NotNull()
            .NotEmpty();
    }
}