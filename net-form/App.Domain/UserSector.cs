using Base.Domain;

namespace App.Domain;

public class UserSector : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } =default!;

    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
}