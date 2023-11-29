using Domain.Common;

namespace Application.Common.Exceptions;

public class InvalidCreatedModifiedValuesException : Exception
{
    public InvalidCreatedModifiedValuesException(string message): base(message) {}

    public static InvalidCreatedModifiedValuesException Create<T>(T entity)
        where T : BaseAuditableEntity
    {
        return new InvalidCreatedModifiedValuesException($"""
            An {typeof(T).Name} with id {entity.Id} was created, but fields '{nameof(BaseAuditableEntity.CreatedBy)}', '{nameof(BaseAuditableEntity.ModifiedBy)}' do not have matching values.
            '{nameof(BaseAuditableEntity.CreatedBy)}' : {entity.CreatedBy}
            '{nameof(BaseAuditableEntity.ModifiedBy)}' : {entity.ModifiedBy}
            """);
    }
}