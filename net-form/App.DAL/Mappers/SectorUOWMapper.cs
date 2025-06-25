using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class SectorUOWMapper : IMapper<App.DAL.DTO.Sector, App.Domain.Sector>
{
    public Domain.Sector? Map(Sector? entity)
    {
        if (entity == null) return null;

        return new Domain.Sector
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentSectorId = entity.ParentSectorId,
            ParentSector = entity.ParentSector != null
                ? new Domain.Sector
                {
                    Id = entity.ParentSector.Id,
                    Name = entity.ParentSector.Name
                }
                : null
        };
    }

    public Sector? Map(Domain.Sector? dto)
    {
        if (dto == null) return null;

        return new Sector
        {
            Id = dto.Id,
            Name = dto.Name,
            ParentSectorId = dto.ParentSectorId
        };
    }
}