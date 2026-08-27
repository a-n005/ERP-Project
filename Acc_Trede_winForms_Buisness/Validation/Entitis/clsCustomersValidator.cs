using Acc_Trede_winForms_Buisness.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Validation.Entitis
{
    internal sealed class clsCustomersValidator:AbstractValidator<clsCustomers_BLL>
    {
        public clsCustomersValidator()
        { 
            RuleFor(x => x.CustomerName).NotEmpty().NotNull().WithMessage("يجب اضافه اسم العميل.");
        }
    }
}
