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
