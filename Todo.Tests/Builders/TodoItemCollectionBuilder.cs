using System.Collections.Generic;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Tests.Builders
{
    public class TodoItemCollectionBuilder
    {
        private readonly List<Importance> items = new();

        public TodoItemCollectionBuilder()
        {
        }

        public TodoItemCollectionBuilder WithItem(Importance importance)
        {
            items.Add(importance);
            return this;
        }

        public IEnumerable<TodoItem> Build()
        {
            var nextId = 1;
            foreach (var item in items)
            {
                yield return new TodoItem(nextId++, "", "", item);
            }
        }
    }
}