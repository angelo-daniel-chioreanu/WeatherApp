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
using System.Windows.Shapes;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for OptionsDialogBox.xaml
    /// </summary>
    public partial class OptionsDialogBox : Window
    {
        public OptionsDialogBox()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public string ZIP { get; set; }
        public string APPID { get; set; }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValid(this))
                return;

            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private bool IsValid(DependencyObject node)
        {
            if (node != null)
            {
                if (Validation.GetHasError(node))
                {
                    if (node is IInputElement)
                        Keyboard.Focus((IInputElement)node);

                    return false;
                }

                foreach(object subnode in LogicalTreeHelper.GetChildren(node))
                {
                    if (subnode is DependencyObject)
                    {
                        if (!IsValid((DependencyObject)subnode))
                            return false;
                    }
                }
            }

            return true;
        }
    }

    public class RegularExpressionValidationRule : ValidationRule
    {
        public string Expression { get; set; }
        public string ErrorMessage { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isMatch = false;

            if (value != null)
            {
                Regex regEx = new Regex(Expression);

                isMatch = regEx.IsMatch(value.ToString());
            }

            return new ValidationResult(isMatch, isMatch ? null : ErrorMessage);
        }
    }
}
