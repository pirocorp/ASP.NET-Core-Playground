namespace InjectingMultipleImplementations.Pages
{
    using InjectingMultipleImplementations.Services;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SingleMessageModel : PageModel
    {
        private readonly IMessageSender _messageSender;

        public SingleMessageModel(IMessageSender messageSender)
        {
            this._messageSender = messageSender;
        }

        public void OnGet(string username)
        {
            this._messageSender.SendMessage(username);
        }
    }
}
