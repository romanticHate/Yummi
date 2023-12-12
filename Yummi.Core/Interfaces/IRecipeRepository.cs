using Yummi.Core.Entities;
using Yummi.Core.Interfaces.Generic;

namespace Yummi.Core.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipesByUser(int userId);
        Task<IEnumerable<Recipe>> GetRecipesByCuisine(string cuisine, int userId);
    }
}
