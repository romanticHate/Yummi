using FluentValidation;
using Yummi.Core.Entities;

namespace Yummi.Persistance.Validators
{
    public class RecipeVldtr: AbstractValidator<Recipe>
    {
        public RecipeVldtr()
        {
            RuleFor(r => r.Name)
            .NotNull().WithMessage("Recipe name should not be null")
            .NotEmpty().WithMessage("Recipe name should not be empty")
            .MaximumLength(24)
            .MinimumLength(3);

            RuleFor(r => r.Cuisine)
            .MaximumLength(3)
            .MinimumLength(3);
        }
    }
}
