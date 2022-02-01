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
        CheckWaldo
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

    private ValidationState CheckWaldo(ValidationState existing)
    {
      if (IsRequired && !txtValue.Text.Contains("Waldo", StringComparison.InvariantCultureIgnoreCase)) existing.FailureMessage = "*The field does not contain waldo!";
      return existing;
    }
  }
}
