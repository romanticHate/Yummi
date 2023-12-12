namespace Yummi.Application.DTOs
{
    public class RecipeIngredientDto
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int? MeasureId { get; set; }
        public decimal? Amount { get; set; }
    }
}
