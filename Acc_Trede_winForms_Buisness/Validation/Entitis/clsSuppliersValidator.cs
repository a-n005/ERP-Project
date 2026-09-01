using Acc_Trede_winForms.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Validation.Entitis
{
    internal sealed class clsSuppliersValidator:AbstractValidator<clsSuppliers_BLL>
    {
        public clsSuppliersValidator()
        {
            RuleFor(x => x.SupplierName).NotEmpty().NotNull().WithMessage("يجب اضافه اسم المورد.");
        }
    }
}
