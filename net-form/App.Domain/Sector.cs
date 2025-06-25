using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Sector : BaseEntity
{
    [Required]
    public string Name { get; set; } = default!;
    public int? ParentSectorId { get; set; }
    public Sector? ParentSector { get; set; }
    
    public List<Sector> Children { get; set; } = new List<Sector>();
}