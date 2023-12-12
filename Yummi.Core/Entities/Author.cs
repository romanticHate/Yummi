namespace Yummi.Core.Entities
{
    public partial class Author: BaseEntity
    {     
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
    }
}
