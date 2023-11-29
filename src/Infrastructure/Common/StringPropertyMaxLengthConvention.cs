using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infrastructure.Common;

public class StringPropertyMaxLengthConvention : IModelFinalizingConvention
{
    private readonly int _stringPropertyMaxLength;

    public StringPropertyMaxLengthConvention(int stringPropertyMaxLength)
    {
        _stringPropertyMaxLength = stringPropertyMaxLength;
    }

    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        var entities = modelBuilder.Metadata.GetEntityTypes()
            .Where(entity => typeof(BaseEntity).IsAssignableFrom(entity.ClrType));

        foreach (var property in entities.SelectMany(entity => entity.GetDeclaredProperties()
                     .Where(p => p.ClrType == typeof(string))))
        {
            property.Builder.HasMaxLength(_stringPropertyMaxLength);
        }
    }
}