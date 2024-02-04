using FluentValidation;

namespace Sample2.Application.Orders.Commands.UpdateOrder;

public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateTodoItemCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(p => p.Status)
            .NotNull()
            .NotEmpty();

        RuleFor(p => p.ShipToAddress)
            .NotNull()
            .NotEmpty();
    }
}