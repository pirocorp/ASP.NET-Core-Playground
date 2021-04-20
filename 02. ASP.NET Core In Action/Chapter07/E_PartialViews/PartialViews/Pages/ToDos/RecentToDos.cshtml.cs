namespace PartialViews.Pages.ToDos
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class RecentToDosModel : PageModel
    {
        public int RecentToDosToDisplay { get; } = 3;

        public IEnumerable<ToDoItemViewModel> RecentToDos { get; set; }

        public void OnGet()
        {
            this.RecentToDos = TaskService.AllTasks.Take(this.RecentToDosToDisplay);
        }
    }
}
