using Acc_Trede_winForms_Buisness.Sales;
using FluentValidation;
using Global;

namespace Acc_Trede_winForms_Buisness.Validation.Sales
{
    public class clsSalesValidator : AbstractValidator<clsSalesInvoices_BLL>
    {
        public enum enValidationMode
        {
            ForAdd,
            ForUpdate
            // يمكنك إضافة حالات أخرى مستقبلاً هنا مثل: ForPost, ForDelete
        }
        public clsSalesValidator(enValidationMode mode)
        {

            switch (mode)
            {
                case enValidationMode.ForAdd:

                    break;
                case enValidationMode.ForUpdate:

                    break;
                default:
                    break;
            }
            // 1. فحص المستخدم والتعريف
            RuleFor(x => x.CreatedBy)
                .Must(_ => GlobalUser.CurrentUser != null)
                .WithMessage("يجب تسجيل الدخول.")
                .GreaterThan(0)
                .WithMessage("بيانات المستخدم غير صحيحة.");

            // 2. فحص السلة والحاوية
            RuleFor(x => x.SalesCart)
                .NotNull()
                .Must(cart => cart != null && cart.Rows.Count > 0)
                .WithMessage("الفاتورة لا تحتوي على أصناف.");

            // 3. الشروط المالية الحسابية
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("إجمالي الفاتورة يجب أن يكون أكبر من صفر.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("قيم الخصم أو الضريبة أو الصافي غير صحيحة.")
                .LessThanOrEqualTo(x => x.TotalAmount)
                .WithMessage("الخصم لا يمكن أن يكون أكبر من إجمالي الفاتورة.");

            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("قيم الخصم أو الضريبة أو الصافي غير صحيحة.");

            RuleFor(x => x.NetAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("قيم الخصم أو الضريبة أو الصافي غير صحيحة.");

            // 4. الدفع والمبالغ
            RuleFor(x => x.CashAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("لا يمكن أن تكون قيمة الدفع سالبة.");

            RuleFor(x => x.CardAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("لا يمكن أن تكون قيمة الدفع سالبة.");

            RuleFor(x => x)
                .Must(x => (x.CashAmount + x.CardAmount) <= x.NetAmount)
                .WithMessage("المبلغ المدفوع أكبر من صافي الفاتورة.");
        }
    }
}
