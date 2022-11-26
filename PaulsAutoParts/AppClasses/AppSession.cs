using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.AppClasses
{
  public class AppSession
  {
    public AppSession(IHttpContextAccessor httpAccessor)
    {
      _httpAccessor = httpAccessor;
    }

    protected readonly IHttpContextAccessor _httpAccessor;

    public int? CustomerId
    {
      get { return _httpAccessor.HttpContext.Session.GetInt32("CustomerId"); }
      set { _httpAccessor.HttpContext.Session.SetInt32("CustomerId", value.Value); }
    }

    public bool IsFromCustomerMaintenance
    {
      get { return Convert.ToBoolean(_httpAccessor.HttpContext.Session.GetString("IsFromCustomerMaintenance")); }
      set { _httpAccessor.HttpContext.Session.SetString("IsFromCustomerMaintenance", value.ToString()); }
    }

    public string CustomerName
    {
      get { return _httpAccessor.HttpContext.Session.GetString("CustomerName"); }
      set { _httpAccessor.HttpContext.Session.SetString("CustomerName", value); }
    }

    public int ItemsInCart
    {
      get {
        return Cart != null ? Cart.Items.Count : 0;
      }
    }

    public void ClearCart()
    {
      _httpAccessor.HttpContext.Session.Remove("Cart");
    }

    public ShoppingCart Cart
    {
      get {
        if (_httpAccessor.HttpContext.Session.GetString("Cart") == null) {
          return null;
        }
        else {
          return JsonSerializer.Deserialize<ShoppingCart>(_httpAccessor.HttpContext.Session.GetString("Cart"));
        }
      }
      set {
        if (value == null) {
          _httpAccessor.HttpContext.Session.Remove("Cart");
        }
        else {
          _httpAccessor.HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(value));
        }
      }
    }

    public ProductSearch LastProductSearch
    {
      get {
        if (_httpAccessor.HttpContext.Session.GetString("LastProductSearch") == null) {
          return null;
        }
        else {
          return JsonSerializer.Deserialize<ProductSearch>(_httpAccessor.HttpContext.Session.GetString("LastProductSearch"));
        }
      }
      set {
        if (value == null) {
          _httpAccessor.HttpContext.Session.Remove("LastProductSearch");
        }
        else {
          _httpAccessor.HttpContext.Session.SetString("LastProductSearch", JsonSerializer.Serialize(value));
        }
      }
    }
  }
}
