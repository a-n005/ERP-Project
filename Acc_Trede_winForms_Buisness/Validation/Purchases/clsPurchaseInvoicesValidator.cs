using Acc_Trede_winForms_Buisness.Purchases;
using FluentValidation;
using Global;

namespace Acc_Trede_winForms_Buisness.Validation.Purchases
{
    public class clsPurchaseInvoicesValidator : AbstractValidator<clsPurchaseInvoices_BLL>
    {
        public enum enMode { ForAdd,ForUpdate}
        public clsPurchaseInvoicesValidator(enMode mode)
        {
            switch (mode)
            {
                case enMode.ForAdd: _Add(); break;
                case enMode.ForUpdate: _Update(); break;
                default:
                    break;
            }
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
            RuleFor(x => x.Cart)
                .NotNull()
                .Must(cart => cart != null && cart.Rows.Count > 0)
                .WithMessage("الفاتورة لا تحتوي على أصناف.");

            // Amount validtion
            RuleFor(x => x.TotalAmount)
                .LessThanOrEqualTo(0)
                .WithMessage("إجمالي الفاتورة يجب أن يكون أكبر من صفر.");

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
            RuleFor(x => x.SupplierInvoiceNum)
                .NotEmpty().WithMessage("رقم الفاتورة يجب ان يحمل قيمه.");

        }
        private void _Update()
        {
            RuleFor(x => x.SupplierInvoiceNum)
                .NotEmpty().WithMessage("لا يمكن ان يكون رقم الفاتورة فارغ.");

            RuleFor(x => x.SupplierID)
                .Null().WithMessage("لا يمكن ان يكون المورد فارغ.")
                .GreaterThan(0).WithMessage("خطاء في بيانات المورد.");
        }
    }
}
