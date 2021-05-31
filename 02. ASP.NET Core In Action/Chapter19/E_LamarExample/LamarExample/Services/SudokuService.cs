namespace LamarExample
{
    public class SudokuService : IGamingService
    {
        private readonly IValidator<AvatarModel> _validator;  

        public SudokuService(IValidator<AvatarModel> validator)
        {
            this._validator = validator;
        }
    }
}
