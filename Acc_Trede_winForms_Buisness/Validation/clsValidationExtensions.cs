using Acc_Trade_Core;
using FluentValidation.Results;
using System.Linq;
using System.Windows.Forms;

namespace Acc_Trede_winForms_Buisness.Validation
{
    public static class clsValidationExtensions
    {
        public static Result ToResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return Result.Success();

            var firstError = validationResult.Errors.FirstOrDefault();

            return Result.Failure(firstError?.ErrorMessage ?? "خطأ في البيانات المدخلة.");
        }
        //public static bool ValidateRequired(this CTextBox textBox, ErrorProvider errorProvider, string errorMessage)
        //{
        //    if (string.IsNullOrWhiteSpace(textBox.Text))
        //    {
        //        errorProvider.SetError(textBox, errorMessage);
        //        return false;
        //    }

        //    errorProvider.SetError(textBox, "");
        //    return true;
        //}
    }
}
