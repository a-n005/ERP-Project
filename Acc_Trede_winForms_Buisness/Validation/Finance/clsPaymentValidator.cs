using Acc_Trede_winForms_Buisness.Finance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Validation.Finance
{
    internal sealed class clsPaymentValidator:AbstractValidator<clsPaymentVouchers_BLL>
    {
        public clsPaymentValidator()
        {
            RuleFor(x => x.VoucherNum).NotEmpty().NotNull().WithMessage("يجب ادخال رقم السند.");

            RuleFor(x => x.Amount).NotNull().GreaterThan(0).WithMessage("يجب ادخال قيمه السند.");

            RuleFor(x => x.PaymentMethod).NotNull().NotEmpty().WithMessage("يجب اختيار طريقه دفع.");

            RuleFor(x => x)
               .Must(x => !(x.SupplierID.HasValue && x.CustomerID.HasValue))
               .WithMessage("لا يمكن اختيار مورد وعميل معاً في نفس السند.");

            RuleFor(x => x)
              .Must(x => !(x.PurchaseID.HasValue && x.SaleReturnID.HasValue))
              .WithMessage("لا يمكن اختيار فاتورة مشتريات و ومرتجع بيع معاَ في نففس السند.");
        }
    }
}
