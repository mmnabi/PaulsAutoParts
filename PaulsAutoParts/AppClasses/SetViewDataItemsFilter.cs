using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PaulsAutoParts.AppClasses
{
  /// <summary>
  /// This filter is used to set the ViewData items on each page
  /// </summary>
  public class SetViewDataItemsFilter : IActionFilter
  {
    public SetViewDataItemsFilter(AppSession session)
    {
      UserSession = session;
    }

    protected AppSession UserSession { get; set; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (UserSession != null) {
        var controller = context.Controller as Controller;
        if (controller != null) {
          controller.ViewData["CustomerName"] = UserSession.CustomerName;
          controller.ViewData["ItemsInCart"] = UserSession.ItemsInCart;
        }
      }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      // Set Items in Cart
      if (UserSession != null) {
        var controller = context.Controller as Controller;
        if (controller != null) {
          controller.ViewData["ItemsInCart"] = UserSession.ItemsInCart;
        }
      }
    }
  }
}
