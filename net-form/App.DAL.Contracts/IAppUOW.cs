using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUOW : IBaseUOW
{
    IUserRepository UserRepository { get; }
    ISectorRepository SectorRepository { get; }
    IUserSectorRepository UserSectorRepository { get; }
    
}