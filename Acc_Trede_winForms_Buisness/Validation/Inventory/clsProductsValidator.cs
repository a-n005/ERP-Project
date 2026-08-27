using Acc_Trede_winForms_Buisness.Inventory;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Validation.Inventory
{
    internal sealed class clsProductsValidator : AbstractValidator<clsProducts_BLL>
    {
        public clsProductsValidator()
        {
            // barcode Validation
            RuleFor(x => x.Barcode)
               .NotEmpty()
               .NotNull()
               .WithMessage("يجب تسجيل باركود.");

            // prodctName validation
            RuleFor(x => x.ProductName)
                .NotNull()
                .NotEmpty()
                .WithMessage("يجب تسجيل اسم.");

            // costPrice validtion
            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("يجب ان تضاف قيمه التكلفة.");

            // salePrice validation
            RuleFor(x => x.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("يجب اضافه سعر بيع موجب.");

            // quantity validation
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(" لا تستطيع اضافه كميه سالبه.");

            // mainQuantity Alert validation
            RuleFor(x => x.MinQuantityAlert)
                .GreaterThanOrEqualTo(0).WithMessage("يجب اضافه حد ادنى.");
        }

       
    }
}
