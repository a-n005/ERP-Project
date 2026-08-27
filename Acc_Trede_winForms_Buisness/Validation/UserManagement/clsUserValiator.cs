using Acc_Trede_winForms_Buisness.UserManagement;
using FluentValidation;
using System.Linq;


namespace Acc_Trede_winForms_Buisness.Validation.UserManagement
{
    internal sealed class clsUserValiator : AbstractValidator<clsUser_BLL>
    {
        public enum enMode { ForAdd, ForUpdate }
        public clsUserValiator(enMode mode)
        {
            switch (mode)
            {
                case enMode.ForAdd: _Add(); break;
                case enMode.ForUpdate: _Update(); break;
            }
        }
        private void _Add()
        {
            // FullName Validation
            RuleFor(x => x.FullName)
               .NotEmpty()
               .NotNull()
               .WithMessage("يجب تسجيل الاسم الكامل.");

            // userName validation
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("يجب تسجيل اسم المستخدم.");

            // phone validtion
            RuleFor(x => x.Phone)
                .MaximumLength(10)
                .MinimumLength(10)
                .WithMessage("يجب ان يكون الرقم مكون من 10 خانات.");

            // password validation
            RuleFor(x => x.PassWordHash)
             .NotNull()
             .NotEmpty()
             .WithMessage("يجب ادخال كلمه مرور.");

            // permissions validation
            RuleFor(x => (int)x.Permissions)
                .NotEqual(0).WithMessage(" يجب اختيار صلاحيه واحده على الاقل.");
        }
        private void _Update()
        {
            // FullName Validation
            RuleFor(x => x.FullName)
               .NotEmpty()
               .NotNull()
               .WithMessage("يجب تسجيل الاسم الكامل.");

            // userName validation
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("يجب تسجيل اسم المستخدم.");

            // phone validtion
            RuleFor(x => x.Phone)
                .MaximumLength(10)
                .MinimumLength(10)
                .WithMessage("يجب ان يكون الرقم مكون من 10 خانات.");

            // permissions validation
            RuleFor(x => (int)x.Permissions)
                .NotEqual(0).WithMessage(" يجب اختيار صلاحيه واحده على الاقل.");
        }

    }
}
