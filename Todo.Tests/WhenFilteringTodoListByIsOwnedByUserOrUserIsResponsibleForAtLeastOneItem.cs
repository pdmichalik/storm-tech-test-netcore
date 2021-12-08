using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;
using Todo.Tests.Builders;
using Xunit;

namespace Todo.Tests
{
    public class WhenFilteringTodoListByIsOwnedByUserOrUserIsResponsibleForAtLeastOneItem
    {
        private static readonly IdentityUser TestUser = new("alice@example.com");
        private static readonly IdentityUser AnotherUser = new("ben@example.com");
        private static readonly TodoList TodoListOwnedByTestUser = new TestTodoListBuilder(TestUser, "shopping")
            .WithItem("bread", Importance.High)
            .WithItem("milk", Importance.Medium)
            .Build();
        private static readonly TodoList TodoListOwnedByAnotherUser = new TestTodoListBuilder(AnotherUser, "shopping")
            .WithItem("bread", Importance.High)
            .WithItem("milk", Importance.Medium)
            .Build();
        private static readonly TodoList TodoListOwnedByAnotherUserButWithItemThatTestUserIsResponsibleFor = new TestTodoListBuilder(AnotherUser, "shopping")
            .WithItem("bread", Importance.High)
            .WithItem("milk", Importance.Medium)
            .WithItem("milk", Importance.Low, TestUser.Id)
            .Build();
        private readonly IApplicationDbContext dbContext;

        public WhenFilteringTodoListByIsOwnedByUserOrUserIsResponsibleForAtLeastOneItem()
        {
            var data = new List<TodoList>
            {
                TodoListOwnedByTestUser, TodoListOwnedByAnotherUser,
                TodoListOwnedByAnotherUserButWithItemThatTestUserIsResponsibleFor
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TodoList>>();
            mockSet.As<IQueryable<TodoList>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TodoList>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TodoList>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TodoList>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.TodoLists).Returns(mockSet.Object);

            dbContext = mockContext.Object;
        }

        [Fact]
        public void FilteredCollectionContainsOnlyTodoListsThatUserIsResponsibleFor()
        {
            var collectionOfTodoListsFiltered = dbContext.RelevantTodoLists(TestUser.Id);

            Assert.Equal(new List<TodoList>{TodoListOwnedByTestUser,
                TodoListOwnedByAnotherUserButWithItemThatTestUserIsResponsibleFor}, collectionOfTodoListsFiltered);
        }
    }
}