namespace ToDoList.Pages
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class SimpleViewModel : PageModel
    {
        private readonly ToDoService _service;

        public ToDoItem ToDo { get; set; }

        public SimpleViewModel(ToDoService service)
        {
            this._service = service;
        }

        public IActionResult OnGet(int id)
        {
            this.ToDo = this._service.AllItems.FirstOrDefault(x => x.Id == id);
            if (this.ToDo == null)
            {
                return this.RedirectToPage("Index");
            }
            return this.Page();

        }
    }
}