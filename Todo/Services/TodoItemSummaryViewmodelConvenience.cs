using System.Collections.Generic;
using System.Linq;
using Todo.Models.TodoItems;

namespace Todo.Services
{
    public static class TodoItemSummaryViewmodelConvenience
    {
        public static IEnumerable<TodoItemSummaryViewmodel> OrderByImportanceDescending(this ICollection<TodoItemSummaryViewmodel> items)
        {
            return items.OrderBy(item => item.Importance);
        }
    }
}