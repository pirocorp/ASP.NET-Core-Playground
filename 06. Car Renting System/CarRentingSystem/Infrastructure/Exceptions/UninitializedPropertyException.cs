namespace CarRentingSystem.Infrastructure.Exceptions
{
    using System;

    public class UninitializedPropertyException : InvalidOperationException
    {
        private const string UninitializedPropertyExceptionMessage = "Uninitialized property: {0}";

        public UninitializedPropertyException(string category)
            : base(string.Format(UninitializedPropertyExceptionMessage, category))
        {
        }
    }
}
