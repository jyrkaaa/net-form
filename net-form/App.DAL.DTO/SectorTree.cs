using Base.Contracts;

namespace App.DAL.DTO;

public class SectorTree
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<SectorTree> Children { get; set; } = new();
}