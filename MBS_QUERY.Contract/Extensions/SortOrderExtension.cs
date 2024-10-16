using MBS_QUERY.Contract.Enumerations;

namespace MBS_QUERY.Contract.Extensions;

public static class SortOrderExtension
{
    public static SortOrder ConvertStringToSortOrder(string? sortOrder)
        => !string.IsNullOrWhiteSpace(sortOrder)
            ? sortOrder.ToUpper().Equals("ASC")
                ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;
}