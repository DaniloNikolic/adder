using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Globalization;
using ValidationToolkit;

namespace Adder
{
    public class AdderViewModel : ValidationToolkit.ViewModel
    {
        public Nullable<double> x
        {
            get { return x_; }
            set 
            {
                Sum = null;
                x_ = value;
                ValidateProperty("x");
                NotifyPropertyChanged("x");
            }
        }

        public Nullable<double> y
        {
            get { return y_; }
            set 
            {
                Sum = null;
                y_ = value;
                ValidateProperty("y");
                NotifyPropertyChanged("y");
            }
        }

        public Nullable<double> Sum
        {
            get { return sum_; }
            set { sum_ = value; NotifyPropertyChanged("Sum"); }
        }

        public ICommand CalculateCommand { get { return CalculateCmd; } }

        public ICommand CalculateCmd = null;  // This will be setup to point to a RelayCommand object.

        // Inputs
        Nullable<double> x_;
        Nullable<double> y_;

        // Outputs
        Nullable<double> sum_;

        public bool CanCalculate(object z)
        {
            return    x.HasValue
                   && y.HasValue 
                   && ErrorCount == 0;
        }

        public virtual void ValidateProperty(string propertyName)
        {
            // We don't use this when relying on ValidationRules or IDataErrorInfo.
        }
    }
}


