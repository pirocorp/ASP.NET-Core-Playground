namespace CarRentingSystem.Data
{
    public static class DataConstants
    {
        public static class Car
        {
            public const int BrandMaxLength = 40;
            public const int BrandMinLength = 2;
            public const int ModelMaxLength = 40;
            public const int ModelMinLength = 2;
            public const int DescriptionMinLength = 10;
            public const int YearMaxValue = 2100;
            public const int YearMinValue = 2000;
        }

        public static class Category
        {
            public const int NameMaxLength = 30;
        }

        public static class Dealer
        {
            public const int NameMaxLength = 30;
            public const int NameMinLength = 2;
            public const int PhoneNumberMaxLength = 30;
            public const int PhoneNumberMinLength = 6;
        }
    }
}
