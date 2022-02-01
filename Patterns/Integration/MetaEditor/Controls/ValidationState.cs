using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEditor.Controls
{
  public struct ValidationState
  {
    public ValidationState() 
    {
      FailureMessage = null;
    }

    public ValidationState(ValidationState existing)
    {
      FailureMessage = existing.FailureMessage;
    }

    public string? FailureMessage { get; set; }

    public bool IsValid => string.IsNullOrWhiteSpace(FailureMessage);
  }

  public static class ValidationStateExtensions
  {
    public static ValidationState Validate(this ValidationState existing, IEnumerable<Func<ValidationState, ValidationState>> validators)
    {
      if (!existing.IsValid)return existing;
      foreach (var v in validators)
      {
        var current = v.Invoke(existing);
        if(!current.IsValid)return current;
      }
      return existing;
    }
  }


}
