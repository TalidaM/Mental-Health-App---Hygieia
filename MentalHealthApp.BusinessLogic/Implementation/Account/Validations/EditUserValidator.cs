using FluentValidation;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Account.Validations
{
    public class EditUserValidator : AbstractValidator<UserVM>
    {
        private readonly UnitOfWork _unitOfWork;
        public EditUserValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.PhoneNumber)
                 //.Matches(@"[0-9]+")
                .Matches(@"^(\+(([0-9]){1,3})[-.])?((((([0-9]){2,3})[-.]){1,2}([0-9]{4,10}))|([0-9]{10}))$").WithMessage("Incorrect phone number")
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            RuleFor(r => r.Tara)
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            RuleFor(r => r.Judet)
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            RuleFor(r => r.Oras)
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            RuleFor(r => r.Username)
                .Must(UsernameNotAlreadyExist).WithMessage("Username already exists")
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            _unitOfWork = unitOfWork;
        }
        public bool UsernameNotAlreadyExist(string username)
        {
            return !_unitOfWork.Utilizatori.Get().Any(iu => iu.Username.Equals(username));

        }
    }
}
