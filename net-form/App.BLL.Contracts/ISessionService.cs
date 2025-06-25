namespace App.BLL.Contracts;

public interface ISessionService
{
    string? GetSessionKey();
    void SetSessionKey(string key);
}