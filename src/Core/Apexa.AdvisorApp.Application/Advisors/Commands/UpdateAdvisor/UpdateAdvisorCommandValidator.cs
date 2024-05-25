using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.UpdateAdvisor
{
    public class UpdateAdvisorCommandValidator : AbstractValidator<UpdateAdvisorCommand>
    {
        public UpdateAdvisorCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
          
            RuleFor(p => p.SIN)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(9).WithMessage("{PropertyName} must be 9 characters.");

            RuleFor(p => p.Address)
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");

            RuleFor(p => p.Phone)
                .Length(8).WithMessage("{PropertyName} must be 8 characters.");

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("{PropertyName} must be Green,Yellow,Red characters.");

        }
    }
}
