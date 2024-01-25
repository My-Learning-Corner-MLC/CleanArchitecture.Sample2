using FluentValidation;
using Sample2.Application.Common.Constants;

namespace Sample2.Application.ProductItems.Commands.CreateProductItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(ProductConst.Rules.NAME_MAX_LENTGH);

        RuleFor(p => p.Price)
            .NotNull()
            .GreaterThan(ProductConst.Rules.MIN_PRICE)
                .WithMessage(ProductConst.ErrorMessages.PRODUCT_PRICE_SHOULD_BE_GREATER_THAN(ProductConst.Rules.MIN_PRICE))
            .LessThan(ProductConst.Rules.MAX_PRICE)
                .WithMessage(ProductConst.ErrorMessages.PRODUCT_PRICE_SHOULD_BE_LESS_THAN(ProductConst.Rules.MAX_PRICE));

        RuleFor(p => p.PictureFileName)
            .MaximumLength(ProductConst.Rules.NAME_MAX_LENTGH);

        RuleFor(p => p.PictureUri)
            .NotNull()
            .NotEmpty()
            .MaximumLength(ProductConst.Rules.URI_MAX_LENGTH);
            
        RuleFor(p => p.ProductTypeId)
            .NotNull();

        RuleFor(p => p.ProductBrandId)
            .NotNull();
    }
}