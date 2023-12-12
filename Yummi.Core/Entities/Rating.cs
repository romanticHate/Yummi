namespace Yummi.Core.Entities
{
    public partial class Rating : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Value { get; set; }
    }
}
