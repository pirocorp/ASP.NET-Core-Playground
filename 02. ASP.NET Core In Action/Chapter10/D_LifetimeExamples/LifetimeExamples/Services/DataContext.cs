namespace LifetimeExamples.Services
{
    using System;

    public class DataContext
    {
        private static readonly Random _rand = new();

        public DataContext()
        {
            //The class will return the same random number for its lifetime
            this.RowCount = _rand.Next(1, 1_000_000_000);
        }

        public int RowCount { get; }
    }

    public class TransientDataContext : DataContext { }

    public class ScopedDataContext : DataContext { }

    public class SingletonDataContext : DataContext { }

}
