using System.Security.Claims;
using Application.Common.Interfaces;

namespace WebAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public int UserId
    {
        get
        {
            var idClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (idClaim is null)
            {
                return 0;
            }

            bool success = int.TryParse(idClaim, out var id);

            return success ? id : 0;
        }
    }

    public string? UserName
    {
        get
        {
            var userName = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return userName;
        }
    }

    public bool IsAuthenticated => UserId != 0;
}