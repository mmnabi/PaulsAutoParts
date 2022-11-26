using System.Collections.Generic;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.ViewModelLayer
{
  /// <summary>
  /// This class should be inherited by all View Model classes
  /// NOTE: Be careful about adding Entity classes as this can cause 
  ///       side effects with Data Annotation validations
  /// </summary>
  public class AppViewModelBase : ViewModelBase
  {
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    
    public string SearchCollapse { get; set; }

    public List<string> Years { get; set; }
    public List<string> Makes { get; set; }
    public List<string> Models { get; set; }
    public List<string> Categories { get; set; }

    public override void Init()
    {
      base.Init();

      SearchCollapse = "collapse";

      Years = new List<string>();
      Makes = new List<string>();
      Models = new List<string>();
      Categories = new List<string>();
    }

    #region RemoveFromCart Method
    public void RemoveFromCart(ShoppingCart cart, int id, int custId)
    {
      var item = cart.Items.Find(ci => ci.ProductId == id && ci.CustomerId == custId);
      if (item != null) {
        cart.Items.Remove(item);
      }
    }
    #endregion

    #region ExpireCart Method
    public void ExpireCart(ShoppingCart cart)
    {
      cart.Items.Clear();
      cart.PromotionCode = new PromoCode();
    }
    #endregion
  }
}