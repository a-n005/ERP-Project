using Acc_Trede_winForms_Buisness.Sales;
using FluentValidation;
using Global;

namespace Acc_Trede_winForms_Buisness.Validation.Sales
{
    internal sealed class clsSalesValidator : AbstractValidator<clsSalesInvoices_BLL>
    {
        public enum enValidationMode
        {
            ForAdd,
            ForUpdate
            // يمكنك إضافة حالات أخرى مستقبلاً هنا مثل: ForPost, ForDelete
        }
        public clsSalesValidator()
        {
            _Add(); 
        }

        private void _Add()
        {
            // User Validation
            RuleFor(x => x.CreatedBy)
               .Must(_ => GlobalUser.CurrentUser != null)
               .WithMessage("يجب تسجيل الدخول.")
               .GreaterThan(0)
               .WithMessage("بيانات المستخدم غير صحيحة.");

            // Cart validation
            RuleFor(x => x.SalesCart)
                .NotNull()
                .Must(cart => cart != null && cart.Rows.Count > 0)
                .WithMessage("الفاتورة لا تحتوي على أصناف.");

            // Amount validtion
            RuleFor(x => x.TotalAmount)
                .LessThanOrEqualTo(0)
                .WithMessage("إجمالي الفاتورة يجب أن يكون أكبر من صفر.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("لا يمكن ان يكون الخصم بالسالب")
                .LessThanOrEqualTo(x => x.TotalAmount).WithMessage("الخصم لا يمكن أن يكون أكبر من إجمالي الفاتورة.");

            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage(" لا يمكن ان تكون الضريبه سالبه .");

            RuleFor(x => x.NetAmount)
                .GreaterThanOrEqualTo(0).WithMessage("لا يمكن ان يكون الصافي اقل من صفر .");

            RuleFor(x => x.CashAmount)
                .GreaterThanOrEqualTo(0).WithMessage("لا يمكن أن تكون قيمة الدفع سالبة.");

            RuleFor(x => x.CardAmount)
                .GreaterThanOrEqualTo(0).WithMessage("لا يمكن أن تكون قيمة الدفع سالبة.");

            RuleFor(x => x)
                .Must(x => (x.CashAmount + x.CardAmount) <= x.NetAmount)
                .WithMessage("المبلغ المدفوع أكبر من صافي الفاتورة.");

            // invoice num validation 
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("رقم الفاتورة يجب ان يحمل قيمه.");

        }
    }
}
