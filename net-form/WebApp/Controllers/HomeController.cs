using App.BLL.Contracts;
using App.DAL;
using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ISessionService _http;
    private readonly IAppUOW _uow;


    public HomeController(ISessionService http, IAppUOW uow)
    {
        _http = http;
        _uow = uow;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        
        var vm = new UserFormViewModel();
        await LoadFromSession(vm);
        var sectors = await _uow.SectorRepository.AllAsync();
        vm.Sectors = SectorTreeBuilder.Build(sectors.ToList());
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserFormViewModel vm)
    {
        vm.Message = string.Empty;
        var sectors = await _uow.SectorRepository.AllAsync();
        vm.Sectors = SectorTreeBuilder.Build(sectors.ToList());
        if (!ValidateUserForm(vm, out var error))
        {
            vm.Message = error;
            return View(vm);
        }
        var sessionGuid = _http.GetSessionKey() ?? null;
        if (sessionGuid == null)
        {
            sessionGuid = Guid.NewGuid().ToString();

            var user = new App.DAL.DTO.User()
            {
                Name = vm.Name,
                AgreedToTerms = vm.AgreeToTerms,
                SessionString = sessionGuid,
                UserSectors = vm.SelectedSectorIds.Select(s => new App.DAL.DTO.UserSector()
                {
                    SectorId = s
                }).ToList()
            };
            _http.SetSessionKey(sessionGuid);
            _uow.UserRepository.Add(user);
            await _uow.SaveChangesAsync();
            vm.Message = $"User '{user.Name}' and sectors added";
        }
        else
        {
            var user = await _uow.UserRepository.FindBySessionAsync(sessionGuid);
            if (user == null)
            {
                return View(vm);
            }
            user.AgreedToTerms = vm.AgreeToTerms;
            user.Name = vm.Name;
            user.UserSectors = new List<App.DAL.DTO.UserSector>();
            user.UserSectors = vm.SelectedSectorIds.Select(s => new App.DAL.DTO.UserSector()
            {
                SectorId = s
            }).ToList();
            await _uow.UserRepository.UpdateAsync(user);
            vm.Message = $"User modified";

        }
        vm.Sectors = SectorTreeBuilder.Build(sectors.ToList());
        return View(vm);
    }

    private async Task LoadFromSession(UserFormViewModel vm)
    {
        var sessionGuid = _http.GetSessionKey() ?? null;
        if (sessionGuid != null)
        {
            var user = await _uow.UserRepository.FindBySessionAsync(sessionGuid);

            if (user != null)
            {
                vm.Name = user.Name;
                vm.AgreeToTerms = user.AgreedToTerms;
                vm.SelectedSectorIds = user.UserSectors!.Select(us => us.Sector!.Id).ToList();
            }
        }
    }
    private bool ValidateUserForm(UserFormViewModel vm, out string message)
    {
        if (!ModelState.IsValid)
        {
            message = "Model state is invalid, try again";
            return false;
        }
        if (string.IsNullOrWhiteSpace(vm.Name))
        {
            message = "Please enter a name";
            return false;
        }
        if (!vm.AgreeToTerms)
        {
            message = "Please agree to terms";
            return false;
        }
        if (vm.SelectedSectorIds.Count == 0)
        {
            message = "Please choose at least one sector";
            return false;
        }
        message = string.Empty;
        return true;
    }
}