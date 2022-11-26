using System.Collections.Generic;

namespace PaulsAutoParts.Common
{
  public class ViewModelBase
  {
    public List<string> Messages { get; set; }
    public string Message { get; set; }
    public bool IsDetailVisible { get; set; }
    public bool IsValid { get; set; }
    public int TotalRecords { get; set; }

    public virtual void Init()
    {
      Messages = new List<string>();
      Message = string.Empty;
      TotalRecords = 0;
    }

    public virtual void Get()
    {
    }

    public virtual void Get(int id)
    {
    }

    public virtual void Get(string id)
    {
    }

    public virtual void Search()
    {
    }

    public virtual void CreateEmptyEntity()
    {
    }

    public virtual bool Save()
    {
      return true;
    }

    public virtual bool Validate()
    {
      return true;
    }

    public virtual bool Delete(int id)
    {
      return true;
    }

    public virtual bool Delete(string id)
    {
      return true;
    }
  }
}