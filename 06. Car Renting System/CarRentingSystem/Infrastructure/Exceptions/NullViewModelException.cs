namespace CarRentingSystem.Infrastructure.Exceptions
{
    using System;

    public class NullViewModelException : NullReferenceException
    {
        private const string NullViewModelExceptionMessage = "Vew Model of type: {0} is null.";

        public NullViewModelException(string viewModel)
            : base(string.Format(NullViewModelExceptionMessage, viewModel))
        {
        }
    }
}
