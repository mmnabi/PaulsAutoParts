using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the Lookup.VehicleType table.
  /// </summary>

  [Table("VehicleType", Schema = "Lookup")]
  public partial class VehicleType : AppEntityBase
  {
    public VehicleType()
    {
    }

    /// <summary>
    /// Get/Set the VehicleTypeId value
    /// </summary>
    [Display(Name = "Vehicle Type Id")]
    [Key]
    public int? VehicleTypeId { get; set; }


    /// <summary>
    /// Get/Set the Year value
    /// </summary>
    [Display(Name = "Year")]
    [Required(ErrorMessage = "Year must be filled in.")]
    public int Year { get; set; }

    /// <summary>
    /// Get/Set the Make value
    /// </summary>
    [Display(Name = "Make")]
    [Required(ErrorMessage = "Make must be filled in.")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Make must be between {2} and {1} characters long.")]
    public string Make { get; set; }

    /// <summary>
    /// Get/Set the Model value
    /// </summary>
    [Display(Name = "Model")]
    [Required(ErrorMessage = "Model must be filled in.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Model must be between {2} and {1} characters long.")]
    public string Model { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{Year}";
    }
    #endregion
  }
}
