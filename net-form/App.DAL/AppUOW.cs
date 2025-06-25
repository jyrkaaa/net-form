using Base.DAL.EF;
using App.DAL.Contracts;
using App.DAL.Repositories;

namespace App.DAL;

public class AppUOW : BaseUOW<AppDbContext> , IAppUOW
    
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    private IUserRepository? _userRepository;
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(UOWDbContext);
    
    private ISectorRepository? _sectorRepository;
    public ISectorRepository SectorRepository => _sectorRepository ??= new SectorRepository(UOWDbContext);
    
    private IUserSectorRepository? _userSectorRepository;
    public IUserSectorRepository UserSectorRepository => _userSectorRepository ??= new UserSectorRepository(UOWDbContext);

}