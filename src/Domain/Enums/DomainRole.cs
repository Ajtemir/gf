using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(EnumerationJsonConverter<DomainRole>))]
public class DomainRole : Enumeration
{
    public DomainRole(int id, string name, string note) : base(id, name)
    {
        Note = note;
    }

    public string Note { get; private set; }

    public static readonly DomainRole Administrator = new(1, nameof(Administrator), "Администратор");
}