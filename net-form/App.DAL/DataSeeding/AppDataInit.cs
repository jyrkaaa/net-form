using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.DataSeeding;

public static class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
    }

    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedSectors(AppDbContext context)
    {
        var sectorDict = new Dictionary<int, App.Domain.Sector>();

        // 1st pass: create and attach all sectors without setting ParentSectorId
        foreach (var (name, id, parentId) in InitialData.Sectors)
        {
            var sector = new App.Domain.Sector
            {
                Id = id,
                Name = System.Net.WebUtility.HtmlDecode(name), // decode &amp; etc
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            };

            context.Sectors.Add(sector);
            sectorDict[id] = sector;
        }

        context.SaveChanges();

        // 2nd pass: now that all IDs exist, set the ParentSectorId
        foreach (var (name, id, parentId) in InitialData.Sectors)
        {
            if (parentId.HasValue && sectorDict.ContainsKey(id))
            {
                var sector = sectorDict[id];
                sector.ParentSectorId = parentId;
            }
        }

        context.SaveChanges();
    }

}
