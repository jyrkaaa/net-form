using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.Mappers;

public class UserSectorUOWMapper : IMapper<App.DAL.DTO.UserSector, App.Domain.UserSector>
{
    public UserSector? Map(Domain.UserSector? entity)
    {
        if (entity == null) return null;
        return new UserSector()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SectorId = entity.SectorId,
            Sector = entity.Sector != null ? new Sector()
            {
                Id = entity.Sector.Id,
                ParentSectorId = entity.Sector.ParentSectorId,
                Name = entity.Sector.Name,
            } : null,
        };
    }

    public Domain.UserSector? Map(UserSector? entity)
    {
        if (entity == null) return null;
        return new App.Domain.UserSector()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SectorId = entity.SectorId,
            Sector = entity.Sector != null ? new App.Domain.Sector()
            {
                Id = entity.Sector.Id,
                ParentSectorId = entity.Sector.ParentSectorId,
                Name = entity.Sector.Name,
            } : null,
        };
    }
}