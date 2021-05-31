namespace LamarExample
{
    public class CrosswordService : IGamingService
    {
        private readonly IValidator<AvatarModel> _validator;  

        public CrosswordService(IValidator<AvatarModel> validator)
        {
            this._validator = validator;
        }
    }
}
