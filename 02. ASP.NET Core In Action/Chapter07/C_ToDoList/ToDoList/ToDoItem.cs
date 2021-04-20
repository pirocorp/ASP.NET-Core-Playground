namespace ToDoList
{
    using System.Collections.Generic;
    
    public class ToDoItem
    {
        public ToDoItem(int id, params string[] tasks)
        {
            this.Id = id;
            this.Tasks = new List<string>(tasks);
        }

        public int Id { get; }

        public bool IsComplete => this.Tasks.Count == 0;

        public List<string> Tasks { get; }
    }
}
