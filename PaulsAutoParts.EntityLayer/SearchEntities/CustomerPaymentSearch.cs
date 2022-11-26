using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class CustomerPaymentSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the CustomerId to search for
    /// </summary>
    [Display(Name = "Customer ID")]
    public int? CustomerId { get; set; }

    /// <summary>
    /// Get/Set the OrderHeaderId to search for
    /// </summary>
    [Display(Name = "Order Number")]
    public int? OrderHeaderId { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

      if (CustomerId.HasValue) {
        ret += $"{comma}Customer ID={CustomerId.Value}";
        comma = ",";
      }
      if (OrderHeaderId.HasValue) {
        ret += $"{comma}OrderHeaderId={OrderHeaderId.Value}";
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
