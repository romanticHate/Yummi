namespace Yummi.Application.DTOs
{
    public class RecipeDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Instructions { get; set; } = null!;
        public string? Cuisine { get; set; }
        public string? Course { get; set; }
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
    }
}
