namespace Yummi.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRecipeRepository RecipeRepository { get; }
        IIngredientRepository IngredientRepository { get; }
        IRecipeIngredientRepository RecipeIngredientRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
