using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class HomeController : AppController
  {
    #region Constructor
    public HomeController(AppSession session, IConfiguration config) : base(session)
    {
      _config = config;
      _session = session;
    }
    #endregion

    #region Private Variables
    private readonly IConfiguration _config;
    private readonly AppSession _session;
    #endregion

    [HttpGet]
    public IActionResult Index()
    {
      HomeViewModel vm = new();

      // Simulate getting Customer ID/Name from user logging in
      InitSessionVariables(_config, _session);

      return View(vm);
    }

    [HttpGet]
    public IActionResult Shopping()
    {
      return View();
    }

    [HttpGet]
    public IActionResult MyAccountMenu()
    {
      HomeViewModel vm = new()
      {
        MenuToDisplay = HomeViewModel.Menus.MyAccount
      };

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      return View("Index", vm);
    }

    [HttpGet]
    public IActionResult MaintenanceMenu()
    {
      HomeViewModel vm = new()
      {
        MenuToDisplay = HomeViewModel.Menus.Maintenance
      };

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      return View("Index", vm);
    }
  }
}
