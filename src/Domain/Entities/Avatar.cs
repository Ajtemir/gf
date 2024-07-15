using System.Buffers.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Avatar
{
    public int Id { get; set; }
    [JsonIgnore]
    public byte[]? Image { get; set; }
    public string? Image64 => Image == null ? null : Convert.ToBase64String(Image); 
    public string? ImageName { get; set; }
    public string? Extension => Path.GetExtension(this.ImageName)?.Remove(0,1);
    public string? ContentType => string.IsNullOrWhiteSpace(Extension) ? null : $"image/{Extension}";
    public Candidate? Candidate { get; set; }
    public Member? Member { get; set; }
}