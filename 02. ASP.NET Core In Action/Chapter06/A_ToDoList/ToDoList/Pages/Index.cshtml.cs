namespace ToDoList.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            this._logger = logger;
        }

        public RedirectToPageResult OnGet()
        {
            return this.RedirectToPage("ToDo/ListCategory",
                new
                {
                    category = "open",
                    username = "andrew"
                });
        }
    }
}
