using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ValidationToolkit;

namespace Adder
{
    public partial class CalculatorViewUsingContentPresenter : ViewBase
    {
        public CalculatorViewUsingContentPresenter()
        {
            InitializeComponent();
        }

        public override void OnLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.RegisterView(this, typeof(AdderViewModel));
            base.OnLoad(sender, e);
        }
    }
}
