using System.Reflection;
using MassTransit;

namespace MBS_QUERY.Infrastructure.DependencyInjection.Extensions;
internal static class NameFormatterExtensions
{
    public static string ToKebabCaseString(this MemberInfo member)
    {
        return KebabCaseEndpointNameFormatter.Instance.SanitizeName(member.Name);
    }
}

internal class KebabCaseEntityNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return typeof(T).ToKebabCaseString();
    }
}