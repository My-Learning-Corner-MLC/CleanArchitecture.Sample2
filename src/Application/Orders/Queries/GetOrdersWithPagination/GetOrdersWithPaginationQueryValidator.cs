using FluentValidation;

namespace Sample2.Application.Orders.Queries.GetOrdersWithPagination;

public class GetOrdersWithPaginationQueryValidator : AbstractValidator<GetOrdersWithPaginationQuery>
{
    public GetOrdersWithPaginationQueryValidator()
    {
        RuleFor(x => x.BuyerID)
            .NotNull()
            .NotEmpty();
            
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
