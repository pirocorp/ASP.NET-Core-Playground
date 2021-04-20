namespace ToDoList.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using System.Collections.Generic;

    public class IndexModel : PageModel
    {
        private readonly ToDoService _service;

        public IndexModel(ToDoService service)
        {
            this._service = service;
        }

        public IEnumerable<ToDoItem> Items { get; set; }
        public void OnGet()
        {
            this.Items = this._service.AllItems;
        }
    }
}
