using System.Collections.Generic;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Tests.Builders
{
    public class TodoItemSummaryViewmodelCollectionBuilder
    {
        private readonly List<Importance> items = new();

        public TodoItemSummaryViewmodelCollectionBuilder()
        {
        }

        public TodoItemSummaryViewmodelCollectionBuilder WithItem(Importance importance)
        {
            items.Add(importance);
            return this;
        }

        public IEnumerable<TodoItemSummaryViewmodel> Build()
        {
            var nextId = 1;
            foreach (var item in items)
            {
                yield return new TodoItemSummaryViewmodel(nextId++, "title", false, null, item);
            }
        }
    }
}