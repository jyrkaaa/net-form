using Base.Contracts;

namespace App.DAL.DTO;

public class User : IDomainId
{
    public string Name { get; set; } = default!;
    public bool AgreedToTerms { get; set; } = false;
    public string? SessionString { get; set; }
    
    public List<UserSector> UserSectors { get; set; } = new List<UserSector>();
    public int Id { get; set; }
}