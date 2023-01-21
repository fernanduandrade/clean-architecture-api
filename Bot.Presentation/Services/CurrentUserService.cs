using Bot.Application.Common.Interfaces;
using System.Security.Claims;

namespace Bot.Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor context) 
        {
            _httpContextAccessor = context;
        }
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
