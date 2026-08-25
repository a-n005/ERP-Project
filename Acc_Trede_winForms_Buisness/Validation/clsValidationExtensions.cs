using Acc_Trade_Core;
using FluentValidation.Results;
using System.Linq;

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
    }
}
