namespace Sample2.Application.Constants;

public static class OrderConst
{
    public static class Rules
    {
    }

    public static class ErrorMessages
    {
        public const string ORDER_ID_AT_LEAST_GREATER_THAN_0 = "Id at least greater than or equal to 0.";
        public const string ORDER_NEED_AT_LEAST_ONE_ITEM = "Order need at least one item.";
        public const string ORDER_COULD_NOT_UPDATE = "Order could only be updated while in status Ordered or Packed";
        public static string ORDER_HAS_PRODUCTS_THAT_DO_NOT_EXIST(string items) => $"The order contains not exists products - id: {items}";
    }
}