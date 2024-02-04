namespace Sample2.Application.Common.Constants;

public static class OrderConst
{
    public static class Rules
    {
    }

    public static class ErrorMessages
    {
        public const string ORDER_ID_AT_LEAST_GREATER_THAN_0 = "Id at least greater than or equal to 0.";
        public const string ORDER_NEED_AT_LEAST_ONE_ITEM = "Order need at least one item.";
        public static string ORDER_CONTAINS_NOT_EXISTS_ITEMS(string items) => $"The order contains not exists items {items}";
    }
}