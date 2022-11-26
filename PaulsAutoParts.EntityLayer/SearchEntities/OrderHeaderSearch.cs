using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class OrderHeaderSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the OrderHeaderId value
    /// </summary>
    [Display(Name = "Order Header Id")]
    public int? OrderHeaderId { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

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
