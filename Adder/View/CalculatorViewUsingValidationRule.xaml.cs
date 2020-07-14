﻿using System;
using System.Windows.Input;
using ValidationToolkit;
using System.Globalization;

namespace Adder
{
    public partial class CalculatorViewUsingValidationRule : ValidationToolkit.ViewBase
    {
        public CalculatorViewUsingValidationRule()
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