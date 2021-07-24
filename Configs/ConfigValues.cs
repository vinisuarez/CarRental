namespace CarRental.Configs
{
    // This class has const that simulate some config system that would be
    // populate by some config system that ideally could be live updated.
    public class ConfigValues
    {
        public const int PremiunPrice = 150;
        public const int BasicPrice = 100;

        public const int ConvertibleDaysForDiscount = 0;
        public const int ConvertibleDiscountPercentile = 0;

        public const int MiniVanDaysForDiscount = 5;
        public const int MiniVanDiscountPercentile = 20;

        public const int SuvDaysForDiscount = 3;
        public const int SuvDiscountPercentile = 30;
    }
}
