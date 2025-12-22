namespace app.Attributes;

using System.ComponentModel.DataAnnotations;

public class NotInPastAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is DateTime date && date < DateTime.UtcNow)
        {
            return new ValidationResult($"{context.DisplayName} field cannot be in the future.");
        }

        return ValidationResult.Success;
    }
}

public class LengthEqualsAttribute(int length) : ValidationAttribute
{
    public int Length => length;
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value is string str && str.Length != length)
        {
            return new ValidationResult($"{context.DisplayName} field must be exactly {length} characters long");
        }

        return ValidationResult.Success;
    }
}