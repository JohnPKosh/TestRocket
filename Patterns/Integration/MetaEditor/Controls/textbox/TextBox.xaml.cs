using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetaEditor.Controls
{
  /// <summary>
  /// Interaction logic for uiTextBox.xaml
  /// </summary>
  public partial class TextBox : UserControl
  {
    #region Initialization

    public TextBox()
    {
      InitializeComponent();
      //DataObject.AddPastingHandler(txtValue, PasteHandler); // TODO: implement pasting logic validation
      this.DataContext = this;
    }

    private const int DEFAULT_MAX_LEN = 20;
    private const double DEFAULT_TEXT_FONT_SIZE = 12D;

    #endregion


    /* ==================== Start TextBox ==================== */
    #region TextBox

    #region Text Value Properties

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBox), new FrameworkPropertyMetadata(string.Empty));

    public string Text
    {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    #endregion

    #region Addtional Meta Properties

    public string? TextWatermark { get; set; }

    public object? MinValue { get; set; }

    public object? MaxValue { get; set; }

    #endregion


    #region Text Layout Properties

    public int TextWidth { get; set; } = (int)(DEFAULT_MAX_LEN * DEFAULT_TEXT_FONT_SIZE * 1.0);

    public int TextMinHeight { get; set; } = 26;

    public Thickness TextMargin { get; set; } = new Thickness(4);

    public VerticalAlignment TextVerticalAlignment { get; set; } = VerticalAlignment.Top;

    public HorizontalAlignment TextHorizontalAlignment { get; set; } = HorizontalAlignment.Left;

    #endregion


    #region Brush Properties

    public Brush? TextBackground { get; set; } = (SolidColorBrush)Application.Current.Resources["MahApps.Brushes.ThemeBackground"]; // null; {x:Null}

    public Brush TextForeground { get; set; } = (SolidColorBrush)Application.Current.Resources["MahApps.Brushes.ThemeForeground"]; //new SolidColorBrush((Color)Application.Current.Resources["MahApps.Colors.Accent"]);

    public Brush TextBorderBrush { get; set; } = (SolidColorBrush)Application.Current.Resources["MahApps.Brushes.TextBox.Border"];

    public Brush InvalidBorderBrush { get; set; } = (SolidColorBrush)Application.Current.Resources["MahApps.Brushes.Control.Validation"];

    public static DependencyProperty ActiveBorderProperty = DependencyProperty.Register("ActiveBorder", typeof(Brush), typeof(TextBox), new FrameworkPropertyMetadata((SolidColorBrush)Application.Current.Resources["MahApps.Brushes.TextBox.Border"]));

    public Brush ActiveBorder
    {
      get { return (Brush)GetValue(ActiveBorderProperty); }
      set { SetValue(ActiveBorderProperty, value); }
    } 

    #endregion


    #region Font Styling Properties

    public FontWeight TextFontWeight { get; set; } = FontWeights.Normal;

    public FontStyle TextFontStyle { get; set; } = FontStyles.Normal;

    public double TextFontSize { get; set; } = DEFAULT_TEXT_FONT_SIZE;

    public FontFamily TextFontFamily { get; set; } = (FontFamily)Application.Current.Resources["MahApps.Fonts.Family.Control"];

    #endregion


    #region Textbox Sizing Properties

    private int maxLength = DEFAULT_MAX_LEN;
    public int MaxLength
    {
      get => maxLength;
      set
      {
        maxLength = value;
        if (maxLength > 36)
        {
          txtValue.TextWrapping = TextWrapping.Wrap;
          MaxLines = (int)Math.Ceiling(maxLength / 36D);
          TextMinHeight = Math.Min((int)(((TextFontSize) * MaxLines)), 500);
          var width = (int)((maxLength / MaxLines) * TextFontSize);
          TextWidth = width;
          txtValue.Width = width;
        }
        else
        {
          TextWidth = (int)(maxLength * TextFontSize);
        }

      }
    }

    public int MinLength { get; set; } = 0;

    public int MinLines { get; set; } = 1;

    public int MaxLines { get; set; } = 1;

    #endregion


    #region Textbox Editing Properties

    public CharacterCasing TextCasing { get; set; } = CharacterCasing.Normal;

    public bool AcceptsReturn { get; set; }

    public bool AcceptsTab { get; set; }

    public bool ClearTextButton { get; set; }

    #endregion


    #endregion
    /* ==================== End TextBox ==================== */


  }
}
