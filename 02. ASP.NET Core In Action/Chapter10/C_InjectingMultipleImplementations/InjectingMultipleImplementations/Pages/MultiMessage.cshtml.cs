namespace InjectingMultipleImplementations.Pages
{
    using System.Collections.Generic;

    using InjectingMultipleImplementations.Services;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class MultiMessageModel : PageModel
    {
        private readonly IEnumerable<IMessageSender> _messageSenders;
        public MultiMessageModel(IEnumerable<IMessageSender> messageSenders)
        {
            this._messageSenders = messageSenders;
        }

        public void OnGet(string username)
        {
            foreach (var messageSender in this._messageSenders)
            {
                messageSender.SendMessage(username);
            }
        }
    }
}
