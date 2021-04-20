namespace ToDoList.Pages.ToDo
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ListCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public IEnumerable<Task> Tasks { get; set; }

        public IActionResult OnGet()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            //TODO: Validate the parameters
            var service = new ToDoService();
            this.Tasks = service
                .GetToDoItems(this.Input.Category, this.Input.Username)
                .Select(x => new Task(x.Number, x.Title));

            return this.Page();
        }


        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Category { get; set; }
        }

        public class Task
        {
            public Task(int id, string description)
            {
                this.Id = id;
                this.Description = description;
            }

            public int Id { get; }
            public string Description { get; set; }
        }
    }
}