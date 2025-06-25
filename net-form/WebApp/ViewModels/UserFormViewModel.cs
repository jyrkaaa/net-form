using App.DAL.DTO;
using Sector = App.Domain.Sector;

namespace WebApp.ViewModels;

public class UserFormViewModel
{
    public string Name { get; set; } = string.Empty;
    public bool AgreeToTerms { get; set; }
    public List<int> SelectedSectorIds { get; set; } = new();
    public List<SectorTree> Sectors { get; set; } = new();
    public String? Message { get; set; } = default!;
}