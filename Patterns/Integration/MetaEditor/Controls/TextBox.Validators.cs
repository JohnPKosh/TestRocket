using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaEditor.Controls
{
  public partial class TextBox
  {

    private void PerformValidation()
    {
      ValidState = new ValidationState().Validate(new[] {
        CheckRequired,
        CheckMin,
        CheckMax
      });

      if (!ValidState.IsValid)
      {
        TextBorder.BorderBrush = InvalidBorderBrush;
        ValidationLabelText = ValidState.FailureMessage;
        ValidationLabel.Content = ValidationLabelText;
      }
      else
      {
        TextBorder.BorderBrush = TextBorderBrush;
        ValidationLabelText = string.Empty;
        ValidationLabel.Content = ValidationLabelText;
      }
    }


    private ValidationState CheckRequired(ValidationState existing)
    {
      if (IsRequired && string.IsNullOrWhiteSpace(txtValue.Text)) existing.FailureMessage = "*The field is required!";
      return existing;
    }

    private ValidationState CheckMin(ValidationState existing)
    {
      var minStr = MinValue?.ToString();
      if (string.IsNullOrWhiteSpace(minStr)) return existing;
      minStr = minStr.Trim();

      var val = txtValue?.Text.Replace("$", String.Empty).Replace(",", String.Empty);
      if (string.IsNullOrWhiteSpace(val))
      {
        existing.FailureMessage = $"*The value must be a date equal to or greater than {minStr} date!";
        return existing;
      }
      val = val.Trim();

      if (minStr.Contains('/') || (minStr.Contains('-') && !minStr.StartsWith("-"))) // check for dates
      {
        if(DateTime.TryParse(minStr, out DateTime minDt))
        {
          if(!string.IsNullOrWhiteSpace(val) && DateTime.TryParse(val, out DateTime valueDt))
          {
            if(valueDt >= minDt) return existing;
          }
          existing.FailureMessage = $"*The value must be a date equal to or greater than {minStr} date!";
          return existing;
        }
      }
      else if(!string.IsNullOrWhiteSpace(val))
      {
        val = val.Replace("$", String.Empty).Replace(",", String.Empty);
        if(decimal.TryParse(minStr, out decimal minDecimal) && decimal.TryParse(val, out decimal valueDecimal))
        {
          if (valueDecimal >= minDecimal) return existing;
        }        
      }
      existing.FailureMessage = $"*The value must be a number equal to or greater than {minStr}!";
      return existing;
    }

    private ValidationState CheckMax(ValidationState existing)
    {
      var maxStr = MaxValue?.ToString();
      if (string.IsNullOrWhiteSpace(maxStr)) return existing;
      maxStr = maxStr.Trim();

      var val = txtValue?.Text.Replace("$", String.Empty).Replace(",", String.Empty);
      if (string.IsNullOrWhiteSpace(val))
      {
        existing.FailureMessage = $"*The value must be equal to or less than {maxStr} date!";
        return existing;
      }
      val = val.Trim();

      if (maxStr.Contains('/') || (maxStr.Contains('-') && !maxStr.StartsWith("-"))) // check for dates
      {
        if (DateTime.TryParse(maxStr, out DateTime maxDt))
        {
          if (!string.IsNullOrWhiteSpace(val) && DateTime.TryParse(val, out DateTime valueDt))
          {
            if (valueDt <= maxDt) return existing;
          }
          existing.FailureMessage = $"*The value must be equal to or less than {maxStr} date!";
          return existing;
        }
      }
      else if (!string.IsNullOrWhiteSpace(val))
      {
        val = val.Replace("$", String.Empty).Replace(",", String.Empty);
        if (decimal.TryParse(maxStr, out decimal minDecimal) && decimal.TryParse(val, out decimal valueDecimal))
        {
          if (valueDecimal <= minDecimal) return existing;
        }
      }
      existing.FailureMessage = $"*The value must be equal to or less than {maxStr}!";
      return existing;
    }
  }
}
