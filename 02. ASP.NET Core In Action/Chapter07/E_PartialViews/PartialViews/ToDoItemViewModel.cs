namespace PartialViews
{
    using System.Collections.Generic;

    public class ToDoItemViewModel
    {
        public ToDoItemViewModel(int id, string title, params string[] tasks)
        {
            this.Id = id;
            this.Tasks = new List<string>(tasks);
            this.Title = title;
        }

        public int Id { get; }

        public string Title { get; }

        public bool IsComplete => this.Tasks.Count == 0;

        public List<string> Tasks { get; }
    }
}
