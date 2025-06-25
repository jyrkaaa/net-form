using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IUserRepository : IBaseRepository<App.DAL.DTO.User>, ICustomRepository
{
}

public interface ICustomRepository
{
    public Task<DTO.User?> FindBySessionAsync(string sessionKey);
    public Task<int> UpdateAsync(DTO.User user);
}

