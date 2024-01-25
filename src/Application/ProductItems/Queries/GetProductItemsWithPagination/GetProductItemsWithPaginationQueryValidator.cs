using FluentValidation;

namespace Sample2.Application.ProductItems.Queries.GetProductItemsWithPagination;

public class GetProductItemsWithPaginationQueryValidator : AbstractValidator<GetProductItemsWithPaginationQuery>
{
    public GetProductItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
