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

        public static class User
        {
            public const int FullNameMaxLength = 50;
            public const int FullNameMinLength = 5;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }
    }
}
