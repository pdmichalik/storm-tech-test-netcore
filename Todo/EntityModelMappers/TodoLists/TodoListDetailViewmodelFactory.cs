using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool undoneOnly = false)
        {
            var todoListItems =
                undoneOnly ? todoList.Items.Where(todoItem => todoItem.IsDone == false) : todoList.Items;
            var items = todoListItems.Select(TodoItemSummaryViewmodelFactory.Create).ToList();
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items);
        }
    }
}