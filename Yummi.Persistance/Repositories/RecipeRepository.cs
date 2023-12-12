using Yummi.Core.Entities;
using Yummi.Core.Interfaces;
using Yummi.Persistance.DataContext;

namespace Yummi.Persistance.Repositories
{
    public class RecipeRepository: GenericRepository<Recipe>, IRecipeRepository
    {
       
        public RecipeRepository(YummiDbContext context):base(context)
        {

           
        }

        //public async Task<IEnumerable<Recipe>> GetRecipesByCuisine(
        //   string cuisine,
        //   int userId) =>
        //   await _entities
        //   .Where(x => x.Cuisine == cuisine &&
        //   x.UserId == userId).ToArrayAsync();
        public Task<IEnumerable<Recipe>> GetRecipesByCuisine(string cuisine, int userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Recipe>> GetRecipesByUser(int userId) =>
        //    await _entities
        //    .Where(x => x.UserId == userId).ToArrayAsync();
        public Task<IEnumerable<Recipe>> GetRecipesByUser(int userId)
        {
            throw new NotImplementedException();
        }

        
       
    }
}
