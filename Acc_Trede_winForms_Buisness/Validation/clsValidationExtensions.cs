using Acc_Trade_Core;
using FluentValidation.Results;
using System;
using System.Data.SqlClient;
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
        public static T? GetNullable<T>(this SqlDataReader reader, string columnName) where T : struct
        {
            object value = reader[columnName];
            return value == DBNull.Value ? null : (T?)Convert.ChangeType(value, typeof(T));
        }

        public static string GetStringSafe(this SqlDataReader reader, string columnName, string defaultValue = null)
        {
            object value = reader[columnName];
            return value == DBNull.Value ? defaultValue : value.ToString();
        }
    }
}
