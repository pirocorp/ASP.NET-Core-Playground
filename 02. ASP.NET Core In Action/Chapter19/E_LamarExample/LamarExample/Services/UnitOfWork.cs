namespace LamarExample
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly int _priority;

        public UnitOfWork(int priority)
        {
            this._priority = priority;
        }
        
        public void BeginTransaction()
        {

        }

        public void Commit()
        {

        }

        public void Rollback()
        {

        }
    }
}
