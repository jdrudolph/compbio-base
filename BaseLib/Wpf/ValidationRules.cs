using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BaseLib.Wpf{
	public class StringToRegexValidationRule : ValidationRule{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo){
			var str = (string) value;
			if (str == null){
				return new ValidationResult(false, "value was null");
			}
			try{
				var regex = new Regex(str);
				return new ValidationResult(true, "");
			} catch (ArgumentException){
				return new ValidationResult(false, $"'{str}' is not a valid Regex");
			}
		}
	}
}