using Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Common;

public class EnumerationValueConverter<T> : ValueConverter<T, string>
    where T : Enumeration
{
    private EnumerationValueConverter() : base(
        clrValue => clrValue.Name,
        dbValue => Enumeration.FromDisplayName<T>(dbValue))
    {
    }

    public static EnumerationValueConverter<T> Create() => new();
}