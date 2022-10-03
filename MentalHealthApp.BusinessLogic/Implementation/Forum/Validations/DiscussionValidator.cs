using FluentValidation;
using MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Forum.Validations
{
    public class DiscussionValidator : AbstractValidator<CreateDiscussionVM>
    {
        public DiscussionValidator()
        {
            RuleFor(r => r.Titlu)
               .NotEmpty().WithMessage("Camp obligatoriu!");
            RuleFor(r => r.Continut)
                .NotEmpty().WithMessage("Camp obligatoriu!");
        }
        public bool NotAlreadyExist(string email)
        {
            return true;
        }
    }
}
