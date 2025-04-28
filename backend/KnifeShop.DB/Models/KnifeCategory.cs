namespace KnifeShop.DB.Models
{
    public class KnifeCategory
    {
        public long KnifeId { get; set; }
        public Knife? Knife { get; set; } 

        public long CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
