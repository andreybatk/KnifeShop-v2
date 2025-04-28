namespace KnifeShop.Contracts.Knife
{
    public class KnifeInfoDto
    {
        public double? OverallLength { get; set; }
        public double? BladeLength { get; set; }
        public double? ButtThickness { get; set; }
        public double? Weight { get; set; }
        public string? HandleMaterial { get; set; }
        public string? Country { get; set; }
        public string? Manufacturer { get; set; }
        public string? SteelGrade { get; set; }
    }
}