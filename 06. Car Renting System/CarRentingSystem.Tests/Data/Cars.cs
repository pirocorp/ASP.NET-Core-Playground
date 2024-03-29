﻿namespace CarRentingSystem.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using CarRentingSystem.Data.Models;

    public static class Cars
    {
        public static IEnumerable<Car> TenPublicCars()
            => Enumerable
                .Range(0, 10)
                .Select(i => new Car("a", "b", "c", "d", i, i, i)
                {
                    IsPublic = true,
                });
    }
}
