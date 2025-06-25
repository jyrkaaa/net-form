using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;

namespace App.BLL;

public class SessionService : ISessionService

{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string SessionKey = "SessionKey";
    
    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetSessionKey()
    {
        return _httpContextAccessor.HttpContext?.Session.GetString(SessionKey);
    }

    public void SetSessionKey(string key)
    {
        _httpContextAccessor.HttpContext?.Session.SetString(SessionKey, key);
    }
}