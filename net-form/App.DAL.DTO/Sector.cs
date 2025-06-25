using Base.Contracts;

namespace App.DAL.DTO;

public class Sector : IDomainId
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int? ParentSectorId { get; set; }
    public Sector? ParentSector { get; set; }
    
    public List<Sector> Children { get; set; } = new List<Sector>();
}