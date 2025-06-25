using App.DAL.Contracts;
using App.DAL.Mappers;
using App.Domain;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace App.DAL.Repositories;

public class SectorRepository : BaseRepository<App.DAL.DTO.Sector, App.Domain.Sector> , ISectorRepository
{
    public SectorRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new SectorUOWMapper())
    {
    }

    public IEnumerable<DTO.Sector> All(int userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<DTO.Sector>> AllAsync(int userId = default)
    {
        var query = GetQuery()
            .Include(c => c.Children);
        return (await query.ToListAsync()).Select(c => Mapper.Map(c!));;
    }
    

    public DTO.Sector? Find(int id, int userId = default)
    {
        throw new NotImplementedException();
    }

    public override async Task<DTO.Sector?> FindAsync(int id, int userId = default)
    {
        var query = await GetQuery()
            .Include(e => e.Children)
            .FirstOrDefaultAsync(e => e.Id == id);
        return Mapper.Map(query);
    }

    public void Add(DTO.Sector entity, int userId = default)
    {
        var efEntity = Mapper.Map(entity);
        if (efEntity == null) return;
        
        RepositoryDbSet.Add(efEntity);
        
    }

    public DTO.Sector Update(Sector entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(Sector entity, Guid userId = default)
    {
        throw new NotImplementedException();
    }
}