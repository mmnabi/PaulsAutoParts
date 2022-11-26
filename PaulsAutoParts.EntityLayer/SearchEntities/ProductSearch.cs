using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties to help with searching
  /// </summary>
  public partial class ProductSearch : AppSearchBase
  {
    /// <summary>
    /// Get/Set the Year value
    /// </summary>
    [Display(Name = "Vehicle Year")]
    public string Year { get; set; }

    /// <summary>
    /// Get/Set the Make value
    /// </summary>
    [Display(Name = "Vehicle Make")]
    public string Make { get; set; }

    /// <summary>
    /// Get/Set the Model value
    /// </summary>
    [Display(Name = "Vehicle Model")]
    public string Model { get; set; }

    /// <summary>
    /// Get/Set the ProductName value
    /// </summary>
    [Display(Name = "Product Name (or Partial)")]
    public string ProductName { get; set; }

    /// <summary>
    /// Get/Set the Category value
    /// </summary>
    [Display(Name = "Category (or Partial)")]
    public string Category { get; set; }

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
      if (!string.IsNullOrEmpty(ProductName)) {
        ret += $"{comma}Product Name={ProductName}";
        comma = ",";
      }
      if (!string.IsNullOrEmpty(Category)) {
        ret += $"{comma}Category={Category}";
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
