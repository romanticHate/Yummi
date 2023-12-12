using Yummi.Core.Entities;
using Yummi.Core.Interfaces;
using Yummi.Persistance.DataContext;

namespace Yummi.Persistance.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(YummiDbContext context):base(context)
        {
            
        }

    }
}
