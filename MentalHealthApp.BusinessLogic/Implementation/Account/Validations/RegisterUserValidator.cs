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
    public class RegisterUserValidator : AbstractValidator<RegisterModel>
    {
        private readonly UnitOfWork _unitOfWork;
        public RegisterUserValidator(UnitOfWork unitOfWork)
        {
            RuleFor(r => r.Email)
                .Must(NotAlreadyExist).WithMessage("Email already registered")
                .NotEmpty().WithMessage("Camp obligatoriu!")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.PasswordHash)
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$").WithMessage("Password must contains minimum 8 characters, minimum one number, minimum one special character and minimum one letter")
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => Convert.ToDateTime(r.BirthDate))
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Invalid Date")
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.PhoneNumberCountryPrefix)
                .MaximumLength(4).WithMessage("Incorect prefix")
                .NotEmpty().WithMessage("Camp Obligatoriu!");
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
            RuleFor(r => r.Categorie)
                .NotEmpty().WithMessage("Camp Obligatoriu!");
            _unitOfWork = unitOfWork;
        }
        public bool NotAlreadyExist(string email)
        {
            return !_unitOfWork.IdentityUsers.Get().Any(iu => iu.Email.Equals(email));

                
        }
        public bool UsernameNotAlreadyExist(string username)
        {
            return !_unitOfWork.Utilizatori.Get().Any(iu => iu.Username.Equals(username));

        }
    }
}
