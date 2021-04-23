namespace LifetimeExamples.Services
{
    public class Repository
    {
        private readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public int RowCount => this._dataContext.RowCount;
    }

    public class ScopedRepository : Repository
    {
        public ScopedRepository(ScopedDataContext generator) : base(generator) { }
    }

    public class TransientRepository : Repository
    {
        public TransientRepository(TransientDataContext generator) : base(generator) { }
    }

    public class SingletonRepository : Repository
    {
        public SingletonRepository(SingletonDataContext generator) : base(generator) { }
    }

    public class CapturingRepository : Repository
    {
        public CapturingRepository(ScopedDataContext generator) : base(generator) { }
    }
}
