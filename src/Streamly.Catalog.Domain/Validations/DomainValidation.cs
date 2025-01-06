using Streamly.Catalog.Domain.Exceptions;

namespace Streamly.Catalog.Domain.Validations;

public static class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
            throw new EntityValidationException($"{fieldName} should not be null.");
    }
}