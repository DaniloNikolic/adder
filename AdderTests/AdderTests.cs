using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adder;
using ValidationToolkit;

namespace AdderTests
{
    [TestClass]
    public class AdderTests
    {
        const double epsilon = 0.00005;

        [TestMethod]
        public void CheckAdditionService()
        {
            double x = 123.0;
            double y = 0.456;
            Assert.AreEqual(123.456, CalculationService.Add(x, y), epsilon, "The calculated value of x + y is not correct");
        }
        
        [TestMethod]
        public void TestAddition()
        {
            AdderViewModel_INotifyDataErrorInfo vm = null;
            try
            {
                vm = (AdderViewModel_INotifyDataErrorInfo)Controller.CreateViewModel(typeof(AdderViewModel_INotifyDataErrorInfo));
            }
            catch(Exception)
            {
            }
            Assert.AreNotEqual(vm, null);

            // One of the fields is null => can't calculate.
            vm.x = null;
            vm.y = 0.456;
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.IsTrue(vm.HasErrors);
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "x: is mandatory");

            // One of the fields is null => can't calculate.
            vm.x = 123.0;
            vm.y = null;
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.IsTrue(vm.HasErrors);
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "y: is mandatory");

            // One of the fields is null => can't calculate.
            vm.x = null;
            vm.y = null;
            Assert.IsTrue(vm.HasErrors);
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "y: is mandatory");

            // Should be able to calculate when both inputs are valid numbers.
            vm.x = 123.0;
            vm.y = 0.456;
            Assert.IsFalse(vm.HasErrors);
            Assert.IsTrue(vm.CanCalculate(null));
            Assert.AreEqual(vm.CurrentValidationError, null);

            Assert.AreEqual(123.456, CalculationService.Add(vm.x.Value, vm.y.Value), epsilon, "The calculated value of x + y is not correct");

            // One of the fields is negative => can't calculate.
            vm.x = -1.0;
            vm.y = 123.0;
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.IsTrue(vm.HasErrors);
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "x: must be non-negative");

            // One of the fields is negative => can't calculate.
            vm.x = 123.0;
            vm.y = -1.0;
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.IsTrue(vm.HasErrors);
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "y: must be non-negative");

            // One of the fields is negative => can't calculate.
            vm.x = -1.0;
            vm.y = -2.0;
            Assert.IsFalse(vm.CanCalculate(null));
            Assert.IsTrue(vm.HasErrors);
            Assert.AreEqual(vm.CurrentValidationError.ToString(), "y: must be non-negative");
        }
    }
}
