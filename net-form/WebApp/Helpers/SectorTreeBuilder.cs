

using App.DAL.DTO;
using Sector = App.Domain.Sector;

namespace WebApp.Helpers;

public static class SectorTreeBuilder
{
    public static List<SectorTree> Build(List<App.DAL.DTO.Sector> flat)
    {
        var lookup = flat.ToLookup(s => s.ParentSectorId);

        List<SectorTree> BuildTree(int? parentId, int depth = 0)
        {
            return lookup[parentId]
                .OrderBy(s => s.Name)
                .Select(s => new SectorTree
                {
                    Id = s.Id,
                    Name = new string('\u00A0', depth * 4) + s.Name,
                    Children = BuildTree(s.Id, depth + 1)
                })
                .ToList();
        }

        return BuildTree(null);
    }
}