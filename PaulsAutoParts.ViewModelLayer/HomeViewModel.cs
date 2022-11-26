namespace PaulsAutoParts.ViewModelLayer
{
  public class HomeViewModel : AppViewModelBase
  {
    public enum Menus
    {
      None,
      MyAccount,
      Maintenance
    }

    #region Properties
    public Menus MenuToDisplay { get; set; }
    #endregion    
  }
}
