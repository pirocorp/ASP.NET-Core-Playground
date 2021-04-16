namespace ConvertingToMVC
{
    using Microsoft.AspNetCore.Mvc;

    public class ToDoController : Controller
    {
        private readonly ToDoService _service;

        public ToDoController(ToDoService service)
        {
            this._service = service;
        }

        public ActionResult Category(string id)
        {
            var items = this._service.GetItemsForCategory(id);
            var viewModel = new CategoryViewModel(items);

            return this.View(viewModel);
        }
    }
}