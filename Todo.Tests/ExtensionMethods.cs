using System.Linq;
using Todo.Data.Entities;
using Todo.Models.TodoItems;
using Todo.Services;
using Xunit;

namespace Todo.Tests
{
    public class ExtensionMethods
    {
        [Theory]
        [InlineData(new[] { Importance.Low, Importance.Low, Importance.High }, new[] { Importance.High, Importance.Low, Importance.Low })]
        [InlineData(new[] { Importance.Low, Importance.Low, Importance.Low }, new[] { Importance.Low, Importance.Low, Importance.Low })]
        [InlineData(new[] { Importance.High, Importance.Medium, Importance.Low }, new[] { Importance.High, Importance.Medium, Importance.Low })]
        [InlineData(new[] { Importance.Low, Importance.Medium, Importance.High }, new[] { Importance.High, Importance.Medium, Importance.Low })]
        public void OrderByImportanceDescending(Importance[] testCaseImportances, Importance[] expected)
        {
            var todoItemSummaryViewmodelsGiven = testCaseImportances.Select(
                importance => new TodoItemSummaryViewmodel(0, "", false, null, importance)).ToList();

            var todoItemSummaryViewmodelsOrderedByImportance = todoItemSummaryViewmodelsGiven.OrderByImportanceDescending();

            Assert.Equal(expected, todoItemSummaryViewmodelsOrderedByImportance.Select(viewmodel => viewmodel.Importance));
        }
    }
}