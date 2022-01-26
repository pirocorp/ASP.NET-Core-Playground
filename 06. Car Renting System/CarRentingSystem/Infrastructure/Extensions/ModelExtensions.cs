namespace CarRentingSystem.Infrastructure.Extensions
{
    using System.Net;

    using CarRentingSystem.Services.Models.Cars;

    public static class ModelExtensions
    {
        public static string ToFriendlyUrl(this ICarModel car)
            => WebUtility.UrlEncode($"{car.Brand}-{car.Model}-{car.Year}");
    }
}
