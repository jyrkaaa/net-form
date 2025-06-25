using Base.Contracts;

namespace App.DAL.DTO;

public class UserSector : IDomainId
{
    public int UserId { get; set; }
    public User User { get; set; } =default!;

    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
    public int Id { get; set; }
}