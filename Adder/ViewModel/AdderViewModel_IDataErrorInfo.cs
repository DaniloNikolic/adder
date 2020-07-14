using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;

using ValidationToolkit;

namespace Adder
{
    public class AdderViewModel_IDataErrorInfo : AdderViewModel, IDataErrorInfo
    {
        public AdderViewModel_IDataErrorInfo()
        {
            base.ErrorsChanged += OnErrorsChanged;
        }

        string ValidateNonNegativeInput(Nullable<double> x, string fieldName, /*string ConstraintID,*/ string ErrorMessage)
        {
            if (x.HasValue && x.Value < 0.0)
                return ErrorMessage;
            else
                return "";
        }

        string ValidateMandatory(Nullable<double> x, string fieldName, /*string ConstraintID,*/ string ErrorMessage)
        {
            if (x.HasValue)
                return "";
            else
                return ErrorMessage;
        }

        public const string ErrorMessage_MustBeNonNegative = " must be non-negative";
        public const string ErrorMessage_Mandatory         = " is mandatory";

        public string this[string propertyName]
        {
            get
            {
                Tracer.LogValidation("IDataErrorInfo.Item validating " + propertyName);
                switch (propertyName)
                {
                    case "x":
                        {
                            string errMsg = nvl(ValidateNonNegativeInput(x, "x", ErrorMessage_MustBeNonNegative),
                                                ValidateMandatory(x, "x", ErrorMessage_Mandatory));

                            Tracer.LogUserDefinedValidation("IDataErrorInfo.Item[x] called. " + GetValidationErrorMessagesAsString());
                            Tracer.LogUserDefinedValidation("IDataErrorInfo.Item[x] - returned error message is '" + errMsg + "'");

                            return errMsg;
                        }

                    case "y":
                        {
                            string errMsg = nvl(ValidateNonNegativeInput(y, "y", ErrorMessage_MustBeNonNegative),
                                                ValidateMandatory(y, "y", ErrorMessage_Mandatory));

                            Tracer.LogUserDefinedValidation("IDataErrorInfo.Item[x] called. " + GetValidationErrorMessagesAsString());
                            Tracer.LogUserDefinedValidation("IDataErrorInfo.Item[x] - returned error message is '" + errMsg + "'");

                            return errMsg;
                        }
                }
                return "";
            }
        }

        // Never called by the framework.
        public string Error 
        { 
            get 
            {
                return "";  
            } 
        }

        string nvl(string s1, string s2)
        {
            return string.IsNullOrEmpty(s1) ? s2 : s1;
        }

#region ErrorBar

        // The "CurrentValidationError" property is kept up-to-date with the "latest" error.
        public void OnErrorsChanged(object sender, DataErrorsChangedEventArgs args)
        {
            Sum = null;
            NotifyPropertyChanged("CurrentValidationError");
            Tracer.LogUserDefinedValidation("OnErrorsChanged called. " + GetValidationErrorMessagesAsString());
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
