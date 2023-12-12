using Yummi.Core.Interfaces;
using Yummi.Persistance.DataContext;
using Yummi.Persistance.Repositories;

namespace Yummi.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YummiDbContext _context;
        private readonly IRecipeRepository? _recipeRepository;
        private readonly IIngredientRepository? _ingredientRepository;
        private readonly IRecipeIngredientRepository? _recipeIngredientRepository;
        public UnitOfWork(YummiDbContext context)
        {
            _context = context;
        }
        public IRecipeRepository RecipeRepository => _recipeRepository ?? new RecipeRepository(_context);
        public IIngredientRepository IngredientRepository => _ingredientRepository ?? new IngredientRepository(_context);      
        public IRecipeIngredientRepository RecipeIngredientRepository => _recipeIngredientRepository ?? new RecipeIngredientRepository(_context);

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
