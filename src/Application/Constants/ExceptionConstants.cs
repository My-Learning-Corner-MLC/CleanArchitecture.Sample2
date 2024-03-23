namespace Sample2.Application.Constants;

public static class ExceptionConst
{
    public static class ErrorMessages
    {
        public const string RESOURCE_CONFLICT = "Resource items conflicted";
        public const string RESOURCE_NOT_FOUND = "Resource not found";
        public const string VALIDATION_FAILURES = "One or more validation failures have occurred.";
    }

    public static class ErrorDescriptions
    {
        public static string COULD_NOT_FOUND_ITEM_WITH_ID(int id) => $"Could not found item with id: {id}";
    }
}