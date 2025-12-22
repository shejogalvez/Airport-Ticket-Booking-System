namespace app.Attributes;

using System.Reflection;
using System.ComponentModel.DataAnnotations;
using app.Model;

public static class ValidationMetadataGenerator
{
    public static IEnumerable<FieldValidationInfo> Generate<T>()
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            var attributes = prop.GetCustomAttributes(true);

            yield return new FieldValidationInfo
            {
                FieldName = prop.Name,
                TypeDescription = GetTypeDescription(prop.PropertyType),
                Constraints = GetConstraints(attributes)
            };
        }
    }

    private static string GetTypeDescription(Type type)
    {
        if (type == typeof(string)) return "Free Text";
        if (type == typeof(int)) return "Integer";
        if (type == typeof(DateTime)) return "Date Time";
        if (type == typeof(DateTime?)) return "Date Time";
        if (type == typeof(FlightClass?)) return "(Economy, Business, FirstClass)";
        if (type == typeof(FlightClass)) return "(Economy, Business, FirstClass)";
        return type.Name;
    }

    private static List<string> GetConstraints(object[] attributes)
    {
        var constraints = new List<string>();

        foreach (var attr in attributes)
        {
            switch (attr)
            {
                case RequiredAttribute:
                    constraints.Add("Required");
                    break;

                case StringLengthAttribute s:
                    constraints.Add($"Max Length: {s.MaximumLength}");
                    break;

                case RangeAttribute r:
                    constraints.Add($"Allowed Range: {r.Minimum} → {r.Maximum}");
                    break;

                case NotInPastAttribute:
                    constraints.Add("Allowed Range: Today → Future");
                    break;
                case LengthEqualsAttribute s:
                    constraints.Add($"Exact length: {s.Length}");
                    break;
            }
        }

        return constraints;
    }
}

public class FieldValidationInfo
{
    public required string FieldName { get; set; }
    public required string TypeDescription { get; set; }
    public required List<string> Constraints { get; set; }
}