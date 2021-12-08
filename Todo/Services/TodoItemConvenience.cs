using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;

namespace Todo.Services
{
    public static class TodoItemConvenience
    {
        public static IEnumerable<TodoItem> OrderByImportanceDescending(this IEnumerable<TodoItem> items)
        {
            return items.OrderBy(item => item.Importance);
        }
    }
}