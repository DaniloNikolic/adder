using System;
using System.Reflection;
using ValidationToolkit;

namespace Adder
{
    public class Controller
    {
        public static void RegisterView(ViewBase view, Type ViewModelType)
        {
            view.DataContext = CreateViewModel(ViewModelType);
        }

        public static ViewModel CreateViewModel(Type ViewModelType)
        {
            Assembly assembly = ViewModelType.Assembly;
            AdderViewModel ViewModel = (AdderViewModel)assembly.CreateInstance(ViewModelType.FullName);
            if (ViewModel == null)
            {
                throw new Exception("Unable to create ViewModel " + ViewModelType.FullName);
            }

            // Setup command processing.
            ViewModel.CalculateCmd = new RelayCommand((object z) =>
            {
                try
                {
                    // Shouldn't get to here if the controls do not have valid values.
                    double x = ViewModel.x.Value;
                    double y = ViewModel.y.Value;

                    ViewModel.Sum = CalculationService.Add(x, y);
                }
                catch (Exception)
                {
                    return;
                }
            },
            ViewModel.CanCalculate);
            
            return ViewModel;
        }

        public void CreateMainWindow()
        {            
            MainWindow = new MainWindow();
            MainWindow.Show();
        }

        MainWindow MainWindow = null;
    }
}

