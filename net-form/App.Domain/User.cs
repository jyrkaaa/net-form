using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class User : BaseEntity
{
    [Required]
    public string Name { get; set; } = default!;
    public bool AgreedToTerms { get; set; } = false;
    public string? SessionString { get; set; }
    
    public List<UserSector> UserSectors { get; set; } = new List<UserSector>();
}