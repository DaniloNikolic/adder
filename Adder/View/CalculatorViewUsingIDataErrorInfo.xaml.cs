using System;
using System.Windows.Input;
using System.Windows.Media;
using ValidationToolkit;

namespace Adder
{
    public partial class CalculatorViewUsingIDataErrorInfo : ViewBase
    {
        public CalculatorViewUsingIDataErrorInfo()
        {
            InitializeComponent();
        }

        public override void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.RegisterView(this, typeof(AdderViewModel_IDataErrorInfo));
            base.OnLoad(sender, e);
        }
    }
}
