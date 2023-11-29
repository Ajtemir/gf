using Application.Common.Extensions;
using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class DateTimeUtcService : IDateTimeUtcService
{
    public DateTime Now => DateTime.Now.SetKindUtc();
}