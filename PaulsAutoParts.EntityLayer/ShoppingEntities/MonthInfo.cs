namespace PaulsAutoParts.EntityLayer
{
  public partial class MonthInfo
  {
    public MonthInfo(short number, string name)
    {
      MonthNumber = number;
      MonthName = name;
    }

    public short MonthNumber { get; set; }
    public string MonthName { get; set; }

    public string MonthNameForDisplay
    {
      get { return MonthNumber.ToString() + " - " + MonthName; }
    }
  }
}
