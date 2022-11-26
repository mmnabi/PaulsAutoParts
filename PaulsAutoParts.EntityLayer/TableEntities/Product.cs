using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the dbo.Product table.
  /// </summary>

  [Table("Product", Schema = "dbo")]
  public partial class Product : AppEntityBase
  {
    public Product()
    {
    }

    /// <summary>
    /// Get/Set the ProductId value
    /// </summary>
    [Display(Name = "Product Id")]
    [Key]
    public int? ProductId { get; set; }

    /// <summary>
    /// Get/Set the ProductName value
    /// </summary>
    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Product Name must be filled in.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Product Name must be between {2} and {1} characters long.")]
    public string ProductName { get; set; }

    /// <summary>
    /// Get the first 25 characters of the Product Name plus a "..." if applicable
    /// </summary>
    [NotMapped]
    public string ProductNameShort
    {
      get {
        string ret = ProductName;
        int max = 25;

        if (!string.IsNullOrEmpty(ret)) {
          if (ret.Length > max) {
            ret = ret.Substring(0, Math.Min(ret.Length, max)) + "...";
          }
        }

        return ret;
      }
    }

    /// <summary>
    /// Get/Set the Category value
    /// </summary>
    [Display(Name = "Category")]
    [Required(ErrorMessage = "Category must be filled in.")]
    [StringLength(20, MinimumLength = 0, ErrorMessage = "Category must be between {2} and {1} characters long.")]
    public string Category { get; set; }

    /// <summary>
    /// Get/Set the Price value
    /// </summary>
    [Display(Name = "Price")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    /// <summary>
    /// Get/Set the YearBegin value
    /// </summary>
    [Display(Name = "Beginning Year")]
    public int YearBegin { get; set; }

    /// <summary>
    /// Get/Set the YearEnd value
    /// </summary>
    [Display(Name = "Ending Year")]
    public int YearEnd { get; set; }

    /// <summary>
    /// Get/Set whether or not this product is in the shopping cart
    /// </summary>
    [NotMapped]
    public bool IsInCart { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{ProductName}";
    }
    #endregion
  }
}
