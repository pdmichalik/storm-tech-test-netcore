using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;
using Todo.Models.TodoItems;
using Todo.Services;
using Todo.Tests.Builders;
using Xunit;

namespace Todo.Tests
{
    public class WhenOrderByImportanceDescending
    {
        [Theory]
        [InlineData(new[] { Importance.Low, Importance.Low, Importance.High }, new[] { Importance.High, Importance.Low, Importance.Low })]
        [InlineData(new[] { Importance.Low, Importance.Low, Importance.Low }, new[] { Importance.Low, Importance.Low, Importance.Low })]
        [InlineData(new[] { Importance.High, Importance.Medium, Importance.Low }, new[] { Importance.High, Importance.Medium, Importance.Low })]
        [InlineData(new[] { Importance.Low, Importance.Medium, Importance.High }, new[] { Importance.High, Importance.Medium, Importance.Low })]
        public void TodoItemOrderedByImportance(Importance[] testCaseImportances, Importance[] expected)
        {
            var todoItemGiven = GetTodoItemList(testCaseImportances);

            var todoItemOrderedByImportance = todoItemGiven.OrderByImportanceDescending();

            Assert.Equal(expected, todoItemOrderedByImportance.Select(viewmodel => viewmodel.Importance));
        }

        private static IEnumerable<TodoItem> GetTodoItemList(IEnumerable<Importance> testCaseImportances)
        {
            var todoItemSummaryViewmodelCollectionBuilder = new TodoItemCollectionBuilder();
            foreach (var importance in testCaseImportances)
            {
                todoItemSummaryViewmodelCollectionBuilder.WithItem(importance);
            }
            return todoItemSummaryViewmodelCollectionBuilder.Build().ToList();
        }
    }
}