namespace Yummi.Core.DTOs
{
    public class RecipeDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string Cuisine { get; set; } = null!;
        public string Course { get; set; } = null!;
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
    }
}
