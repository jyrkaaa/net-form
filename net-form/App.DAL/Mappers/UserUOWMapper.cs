using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.Contracts;
using Sector = App.Domain.Sector;

namespace App.DAL.Mappers;

public class UserUOWMapper : IMapper<App.DAL.DTO.User, App.Domain.User>
{
    public Domain.User? Map(User? dto)
    {
        if (dto == null) return null;

        var user = new Domain.User
        {
            Id = dto.Id,
            Name = dto.Name,
            SessionString = dto.SessionString,
            AgreedToTerms = dto.AgreedToTerms,
            UserSectors = dto.UserSectors.Select(s => new Domain.UserSector
            {
                Id = s.Id,
                UserId = s.UserId,
                SectorId = s.SectorId,
                Sector = s.Sector != null ? new Sector {Id = s.Sector!.Id, Name = s.Sector.Name } : null,
            }).ToList(),
        };

        return user;
    }

    public User? Map(Domain.User? dto)
    {
        if (dto == null) return null;

        var user = new User
        {
            Id = dto.Id,
            Name = dto.Name,
            AgreedToTerms = dto.AgreedToTerms,
            SessionString = dto.SessionString,
            UserSectors = dto.UserSectors.Select(s => new UserSector
            {
                Id = s.Id,
                UserId = s.UserId,
                SectorId = s.SectorId,
                Sector = s.Sector != null ? new DTO.Sector {Id = s.Sector!.Id, Name = s.Sector.Name } : null,
            }).ToList(),
        };

        return user;
    }
}