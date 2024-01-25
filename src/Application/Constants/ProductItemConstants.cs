namespace Sample2.Application.Common.Constants;

public static class ProductConst
{
    public static class Rules
    {
        public const int NAME_MAX_LENTGH = 50;
        public const int URI_MAX_LENGTH = 300;
        public const decimal MAX_PRICE = 9999999999999999.99M;
        public const decimal MIN_PRICE = 0;
    }

    public static class ErrorMessages
    {
        public const string PRODUCT_NAME_ALREADY_EXISTS = "Product name already exists.";
        public const string PRODUCT_ID_AT_LEAST_GREATER_THAN_0 = "Id at least greater than or equal to 0.";
        public static string PRODUCT_PRICE_SHOULD_BE_GREATER_THAN(decimal price) => $"Product price should be greater than {price}";
        public static string PRODUCT_PRICE_SHOULD_BE_LESS_THAN(decimal price) => $"Product price should be less than {price}";
    }
}