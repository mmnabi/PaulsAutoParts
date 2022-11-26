using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.AppClasses
{
  public class AppController : Controller
  {
    public AppController(AppSession session)
    {
      UserSession = session;
    }

    protected AppSession UserSession { get; set; }

    protected void InitSessionVariables(IConfiguration config, AppSession session)
    {
      // Simulate getting Customer ID/Name from user logging in
      if (!session.CustomerId.HasValue) {
        session.CustomerId = config.GetValue<int>("PaulsAutoParts:CustomerId");
        session.CustomerName = config.GetValue<string>("PaulsAutoParts:CustomerName");
        ViewData["CustomerName"] = UserSession.CustomerName;
      }
      if (session.Cart == null) {
        session.Cart = new();
      }
      if (session.LastProductSearch == null) {
        session.LastProductSearch = new();
      }
    }

    protected void SetViewModelFromSession(AppViewModelBase vm, AppSession session)
    {
      vm.CustomerId = session.CustomerId;
      vm.CustomerName = session.CustomerName;
    }
  }
}
