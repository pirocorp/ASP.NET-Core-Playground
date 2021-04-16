using System;
using System.Collections.Generic;
namespace ATypicalRazorPage
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    // Razor Pages are considered by someone to be MVC pattern
    // The Model is data to be displayed it is accessed via ToDoService and is stored List<ToDoListModel> Items
    // The View is the template that displays the data provided by the model. Category.cshtml template
    // The Controller updates the model and provides the data for display to the view. This role is taken by the OnGet method.

    // You can think of each Razor Page handler as a mini controller focused on a single page.
    // Although there are many different controllers, the handlers all interact with the same application model.
    public class CategoryModel : PageModel
    {
        private readonly ToDoService _service;

        public CategoryModel(ToDoService service)
        {
            this._service = service;
        }

        public List<ToDoListModel> Items { get; set; }

        public IActionResult OnGet(string category)
        {
            this.Items = this._service.GetItemsForCategory(category);
            return this.Page();
        }
    }
}