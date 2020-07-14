using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;

using ValidationToolkit;

namespace Adder
{
    public class AdderViewModel_INotifyDataErrorInfo : AdderViewModel, INotifyDataErrorInfo
    {
        // Constraints
        public const string Constraint_Mandatory = "IsMandatory";
        public const string Constraint_MustBeNonNegative = "NonNegative";

        public AdderViewModel_INotifyDataErrorInfo()
        {
            base.ErrorsChanged += OnErrorsChanged;
        }

        void ValidateNonNegative(Nullable<double> x, string fieldName)
        {
            if (x.HasValue && x.Value < 0.0)
                AddError(new ValidationError(fieldName, Constraint_MustBeNonNegative, fieldName + ": must be non-negative"));
            else
                RemoveError(fieldName, Constraint_MustBeNonNegative);
        }

        void ValidateMandatory(Nullable<double> x, string fieldName)
        {
            if (!x.HasValue)
                AddError(new ValidationError(fieldName, Constraint_Mandatory, fieldName + ": is mandatory"));
            else
                RemoveError(fieldName, Constraint_Mandatory);
        }

        public override void ValidateProperty(string propertyName)
        {
            Tracer.LogValidation("INotifyDataErrorInfo.ValidateProperty called. Validating " + propertyName);
            switch (propertyName)
            {
                case "x":
                    {
                        ValidateNonNegative(x, "x");
                        ValidateMandatory(x, "x");
                    }
                    break;

                case "y":
                    {
                        ValidateNonNegative(y, "y");
                        ValidateMandatory(y, "y");
                    }
                    break;
            }
            if (String.IsNullOrEmpty(propertyName))
            {
                Tracer.LogValidation("No cross-property validation errors.");
            }
        }

#region INotifyErrorDataInfo

        // INotifyErrorDataInfo.
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return base.GetPropertyErrors(propertyName);
        }

        // INotifyErrorDataInfo.
        public bool HasErrors
        {
            get { return ErrorCount != 0; }
        }

        // Helper
        private void RaiseErrorsChanged(string propertyName)
        {
            NotifyErrorsChanged(propertyName);
        }
#endregion

#region CurrentValidationError

        // The "CurrentValidationError" property is kept up-to-date with the "latest" error.
        public void OnErrorsChanged(object sender, DataErrorsChangedEventArgs args)
        {
            Sum = null;
            string propertyName = args.PropertyName;
            Tracer.LogUserDefinedValidation("OnErrorsChanged called. " + GetValidationErrorMessagesAsString());
            NotifyPropertyChanged("CurrentValidationError");
        }
        
        // Bind target for error bar.
        public virtual ValidationError CurrentValidationError
        {
            get
            {
                if (ErrorCount == 0)
                    return null;

                // Get the error list associated with the last property to be validated.
                Debug.Assert(!String.IsNullOrEmpty(lastPropertyValidated));
                List<ValidationError>.Enumerator p = errors[lastPropertyValidated].GetEnumerator();

                // Decide which error needs to be returned.
                ValidationError error = null;
                while (p.MoveNext())
                {
                    error = p.Current;
                    if (error.ID == "System.Windows.Controls.ExceptionValidationRule")
                        break;
                }
                return error;
            }
        }
#endregion
    }
}


