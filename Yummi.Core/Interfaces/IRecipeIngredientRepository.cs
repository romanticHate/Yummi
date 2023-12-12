using Yummi.Core.DTOs;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces.Generic;

namespace Yummi.Core.Interfaces
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        Task<List<IngredientAmountDto>> GetAllRecipeIngredients(string recipeName);
    }
}
