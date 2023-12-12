namespace Yummi.Core.Entities
{
    public partial class Ingredient:BaseEntity
    {       
        public string Name { get; set; } = null!;
        public string? Calories { get; set; }
    }
}
