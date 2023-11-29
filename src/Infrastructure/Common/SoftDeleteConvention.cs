using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infrastructure.Common;

public class SoftDeleteConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(IConventionModelBuilder builder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entity in builder.Metadata.GetEntityTypes())
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(entity.ClrType))
            {
                var param = Expression.Parameter(entity.ClrType, "param");
                var body = Expression.Equal(Expression.Property(param, nameof(ISoftDeleteEntity.IsDeleted)), Expression.Constant(false));
                entity.Builder.HasQueryFilter(Expression.Lambda(body, param));
            }
        }
    }
}