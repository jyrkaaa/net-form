using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.DAL.Repositories;

public class UserRepository : BaseRepository<DTO.User, App.Domain.User> , IUserRepository
{
    public UserRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserUOWMapper())
    {
    }

    public IEnumerable<DTO.Sector> All(int userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.User>> AllAsync(int userId = default)
    {
        var query = GetQuery()
            .Include(c => c.UserSectors).ThenInclude(s => s.Sector);
        return (await query.ToListAsync()).Select(c => Mapper.Map(c!));;
    }

    public DTO.Sector? Find(int id, int userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.User?> FindAsync(int id, int userId = default)
    {
        var query = await GetQuery()
            .Include(e => e.UserSectors).ThenInclude(s => s.Sector)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Mapper.Map(query);
    }
    public async Task<DTO.User?> FindBySessionAsync(string sessionString)
    {
        var query = await GetQuery()
            .Include(e => e.UserSectors).ThenInclude(s => s.Sector)
            .AsNoTracking().FirstOrDefaultAsync(e => e.SessionString == sessionString);
        return Mapper.Map(query);
    }
    

    public override void Add(DTO.User entity, int userId = default)
    {
        var efEntity = Mapper.Map(entity);
        if (efEntity == null) return;
        
        RepositoryDbSet.Add(efEntity);
        
    }

    public DTO.User Update(User entity)
    {
        throw new NotImplementedException();
    }
    public async Task<int> UpdateAsync(DTO.User entity)
    {
        var userEntity = RepositoryDbSet.Include(s => s.UserSectors).FirstOrDefault(e => e.Id == entity.Id);
        if (userEntity == null) return 0;
        userEntity.UserSectors = entity.UserSectors.Select(s => new UserSector { UserId = s.UserId, SectorId = s.SectorId }).ToList();
        userEntity.Name = entity.Name;
        RepositoryDbSet.Update(userEntity);
        return await RepositoryDbContext.SaveChangesAsync();
    }
    

    public void Remove(User entity, int userId = default)
    {
        throw new NotImplementedException();
    }
}