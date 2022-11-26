using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.Common
{
  public class EntityStringPKBase
  {
    [NotMapped]
    public bool IsAdding { get; set; }
  }
}