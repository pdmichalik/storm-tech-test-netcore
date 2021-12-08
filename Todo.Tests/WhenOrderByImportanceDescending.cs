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
        public void TodoItemSummaryViewmodelsOrderedByImportance(Importance[] testCaseImportances, Importance[] expected)
        {
            var todoItemSummaryViewmodelsGiven = GetTodoItemSummaryViewmodelList(testCaseImportances);

            var todoItemSummaryViewmodelsOrderedByImportance = todoItemSummaryViewmodelsGiven.OrderByImportanceDescending();

            Assert.Equal(expected, todoItemSummaryViewmodelsOrderedByImportance.Select(viewmodel => viewmodel.Importance));
        }

        private static List<TodoItemSummaryViewmodel> GetTodoItemSummaryViewmodelList(IEnumerable<Importance> testCaseImportances)
        {
            var todoItemSummaryViewmodelCollectionBuilder = new TodoItemSummaryViewmodelCollectionBuilder();
            foreach (var importance in testCaseImportances)
            {
                todoItemSummaryViewmodelCollectionBuilder.WithItem(importance);
            }
            return todoItemSummaryViewmodelCollectionBuilder.Build().ToList();
        }
    }
}