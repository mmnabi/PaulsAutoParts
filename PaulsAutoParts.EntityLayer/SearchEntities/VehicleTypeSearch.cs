using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class VehicleTypeSearch : AppSearchBase
  {
    [Display(Name = "Make of Vehicle (or partial)")]
    public string Make { get; set; }

    [Display(Name = "Model of Vehicle (or partial)")]
    public string Model { get; set; }

    [Display(Name = "Year of Vehicle")]
    public string Year { get; set; }

    #region ToString Override
    public override string ToString()
    {
      string ret = string.Empty;
      string comma = string.Empty;

      if (!string.IsNullOrEmpty(Make)) {
        ret += $"{comma}Make={Make}";
        comma = ",";
      }
      if (!string.IsNullOrEmpty(Model)) {
        ret += $"{comma}Model={Model}";
        comma = ",";
      }
      if (!string.IsNullOrEmpty(Year)) {
        ret += $"{comma}Year={Year}";
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
