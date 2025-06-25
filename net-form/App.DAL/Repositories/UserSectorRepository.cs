using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.DAL.Repositories;

public class UserSectorRepository : BaseRepository<DTO.UserSector, App.Domain.UserSector> , IUserSectorRepository
{
    public UserSectorRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new UserSectorUOWMapper())
    {
    }

    public IEnumerable<DTO.UserSector> All(Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DTO.UserSector>> AllAsync(Guid userId = default)
    {
        var query = GetQuery()
            .Include(c => c.Sector);
        return (await query.ToListAsync()).Select(c => Mapper.Map(c!));;
    }

    public DTO.UserSector? Find(Guid id, Guid userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.UserSector?> FindAsync(int id, int userId = default)
    {
        var query = await GetQuery()
            .Include(e => e.Sector)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Mapper.Map(query);
    }

    public override void Add(DTO.UserSector entity, int userId = default)
    {
        var efEntity = Mapper.Map(entity);
        if (efEntity == null) return;
        
        RepositoryDbSet.Add(efEntity);
        
    }

    public DTO.UserSector Update(UserSector entity)
    {
        throw new NotImplementedException();
    }
    
    public void Remove(UserSector entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}