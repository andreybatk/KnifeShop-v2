namespace KnifeShop.DB.Models
{
    public class KnifeInfo
    {
        public KnifeInfo(double? overallLength, double? bladeLength, double? buttThickness, double? weight, string? handleMaterial, string? country, string? manufacturer, string? steelGrade)
        {
            OverallLength = overallLength;
            BladeLength = bladeLength;
            ButtThickness = buttThickness;
            Weight = weight;
            HandleMaterial = handleMaterial;
            Country = country;
            Manufacturer = manufacturer;
            SteelGrade = steelGrade;
        }

        public long Id { get; set; }
        /// <summary>
        /// Общая длина (мм)
        /// </summary>
        public double? OverallLength { get; set; }
        /// <summary>
        /// Длина клинка (мм)
        /// </summary>
        public double? BladeLength { get; set; }
        /// <summary>
        /// Толщина обуха (мм)
        /// </summary>
        public double? ButtThickness { get; set; }
        /// <summary>
        /// Вес (г)
        /// </summary>
        public double? Weight { get; set; }
        /// <summary>
        /// Материал рукояти
        /// </summary>
        public string? HandleMaterial { get; set; }
        /// <summary>
        /// Страна производитель
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        /// Производитель
        /// </summary>
        public string? Manufacturer { get; set; }
        /// <summary>
        /// Марка стали
        /// </summary>
        public string? SteelGrade { get; set; }
    }
}
