using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class OrderDetailSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the OrderHeaderId value
    /// </summary>
    [Display(Name = "Order Number")]
    public int? OrderHeaderId { get; set; }

    /// <summary>
    /// Get/Set the ProductName value
    /// </summary>
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

      if (OrderHeaderId.HasValue) {
        ret += $"{comma}OrderHeaderId={OrderHeaderId.Value}";
        comma = ",";
      }
      if (!string.IsNullOrEmpty(ProductName)) {
        ret += $"{comma}Product Name={ProductName}";
        comma = ",";
      }
      if (string.IsNullOrEmpty(ret)) {
        ret = NoFilterAppliedMessage;
      }
      else {
        ret = $"{ret}";
      }

      return ret;
    }
    #endregion
  }
}
