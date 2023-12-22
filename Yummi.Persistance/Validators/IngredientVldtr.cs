using FluentValidation;
using Yummi.Core.Entities;

namespace Yummi.Persistance.Validators
{
    public class IngredientVldtr:AbstractValidator<Ingredient>
    {
        public IngredientVldtr()
        {
            RuleFor(r => r.Name)
           .NotNull().WithMessage("Ingredient name should not be null")
           .NotEmpty().WithMessage("Ingredient name should not be empty")
           .MaximumLength(24)
           .MinimumLength(3);
        }
    }
}
