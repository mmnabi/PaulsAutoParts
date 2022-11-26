using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class CustomerSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the Last value
    /// </summary>
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

      if (!string.IsNullOrEmpty(LastName)) {
        ret += $"{comma}Last Name={LastName}";
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
