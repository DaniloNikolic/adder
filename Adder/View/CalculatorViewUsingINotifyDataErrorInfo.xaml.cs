using System;
using System.Windows.Input;
using System.Windows.Media;
using ValidationToolkit;

namespace Adder
{
    public partial class CalculatorViewUsingINotifyDataErrorInfo : ViewBase
    {
        public CalculatorViewUsingINotifyDataErrorInfo()
        {
            InitializeComponent();
        }

        public override void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.RegisterView(this, typeof(AdderViewModel_INotifyDataErrorInfo));
            base.OnLoad(sender, e);
        }
    }
}
