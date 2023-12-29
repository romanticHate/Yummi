using Microsoft.EntityFrameworkCore;
using Yummi.Core.DTOs;
using Yummi.Core.Entities;
using Yummi.Core.Interfaces;
using Yummi.Persistance.DataContext;

namespace Yummi.Persistance.Repositories
{
    public class RecipeIngredientRepository : GenericRepository<RecipeIngredient>,
        IRecipeIngredientRepository
    {
        private readonly YummiDbContext _context;
        public RecipeIngredientRepository(YummiDbContext context) : base(context)
        {
            _context = context;
        }    
        public async Task<List<IngredientAmountDto>> GetAllRecipeIngredients(string recipeName)
        {            
            var query = await (from r in _context.Set<Recipe>()
                               join ri in _context.Set<RecipeIngredient>()
                               on r.Id equals ri.RecipeId
                               join i in _context.Set<Ingredient>()
                               on ri.IngredientId equals i.Id
                               join m in _context.Set<Measure>()
                               on ri.MeasureId equals m.Id
                               where r.Name == recipeName
                               select new { i.Name, ri.Amount })
                               .ToListAsync();


            var lstRecipeIngredients = new List<IngredientAmountDto>();
          
            foreach (var i in query)
            {
                var item = new IngredientAmountDto { Amount = Convert.ToInt32(i.Amount),
                    Name = i.Name};               

                lstRecipeIngredients.Add(item);               
            };
           
            return lstRecipeIngredients
                .OrderBy(ri => ri.Name)
                .ToList();                     
        }

       
    }
}
