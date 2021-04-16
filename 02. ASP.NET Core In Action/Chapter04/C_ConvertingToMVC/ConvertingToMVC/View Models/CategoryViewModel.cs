namespace ConvertingToMVC
{
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public List<ToDoListModel> Items { get; set; }

        public CategoryViewModel(List<ToDoListModel> items)
        {
            this.Items = items;
        }
    }
}
