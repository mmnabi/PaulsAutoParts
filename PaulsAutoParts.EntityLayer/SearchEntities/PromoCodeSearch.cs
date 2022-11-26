using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class PromoCodeSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the Code value
    /// </summary>
    [Display(Name = "Promotional Code")]
    public string Code { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

      if (!string.IsNullOrEmpty(Code)) {
        ret += $"{comma}Promotional Code={Code}";
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
