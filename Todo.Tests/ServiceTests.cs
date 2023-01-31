using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Domain;
using Todo.Services;

namespace Todo.Tests
{
    /// <summary>
    /// Tests for services.
    /// </summary>
    [TestFixture]
    public class ServiceTests
    {
        private Mock<ITodoListRepository> todoListRepo;
        private Mock<ITodoEntryRepository> todoEntryRepo;
        private TodoService todoService;

        [SetUp]
        public void SetUp()
        {
            todoListRepo = new Mock<ITodoListRepository>();
            todoEntryRepo = new Mock<ITodoEntryRepository>();
            todoService = new TodoService(todoEntryRepo.Object, todoListRepo.Object);
        }

        /// <summary>
        /// Checks if GetListByIdAsync method returns todo list.
        /// </summary>
        [Test]
        public async Task GetListByIdAsync_Should_Return_Todo_List()
        {
            // Arrange
            int todoListId = 1;
            string todoListTitle = "My Shopping List";

            var list = new TodoList
            {
                Id = todoListId,
                Title = todoListTitle,
            };

            todoListRepo.Setup(x => x.GetByIdAsync(todoListId))
                .ReturnsAsync(list);

            // Act
            var todoList = await todoService.GetListByIdAsync(todoListId);

            // Assert
            Assert.AreEqual(todoListId, todoList.Id);
        }

        /// <summary>
        /// Checks if GetEntriesByListIdAsync method returns entries.
        /// </summary>
        [Test]
        public async Task GetEntriesByListIdAsync_Should_Retrun_All_Todo_Entries_In_List()
        {
            // Arrange
            var listId = 1;
            var list = new TodoList
            {
                Id = listId,
                Title = "My Shopping List",
            };

            var entry1 = new TodoEntry
            {
                Id = 1,
                Title = "My entry",
                TodoListId = listId
            };

            var entry2 = new TodoEntry
            {
                Id = 2,
                Title = "My entry 2nd",
                TodoListId = listId
            };

            list.ToDoEntries.Add(entry1);
            list.ToDoEntries.Add(entry2);

            todoEntryRepo.Setup(x => x.GetByListIdAsync(listId))
                .ReturnsAsync(list.ToDoEntries);

            // Act
            var todoEntries = await todoService.GetEntriesByListIdAsync(listId, hideCompleted: false);

            // Assert
            Assert.AreEqual(list.ToDoEntries.Count, todoEntries.Count);
        }

        /// <summary>
        /// Checks if GetEntriesByListIdAsync method returns entries.
        /// </summary>
        [Test]
        public async Task GetEntriesByListIdAsync_Should_Retrun_All_Non_Completed_Todo_Entries_In_List()
        {
            // Arrange
            var listId = 1;
            var list = new TodoList
            {
                Id = listId,
                Title = "My Shopping List",
            };

            var entry1 = new TodoEntry
            {
                Id = 1,
                Title = "My entry",
                TodoListId = listId,
                Status = Status.InProgress
            };

            var entry2 = new TodoEntry
            {
                Id = 2,
                Title = "My entry 2nd",
                TodoListId = listId,
                Status = Status.Completed
            };

            var entry3 = new TodoEntry
            {
                Id = 2,
                Title = "My entry 2nd",
                TodoListId = listId,
                Status = Status.NotStarted
            };

            list.ToDoEntries.Add(entry1);
            list.ToDoEntries.Add(entry2);
            list.ToDoEntries.Add(entry3);

            todoEntryRepo.Setup(x => x.GetByListIdAsync(listId))
                .ReturnsAsync(list.ToDoEntries.Where(todoEntry => todoEntry.Status != Status.Completed).ToList());

            // Act
            var todoEntries = await todoService.GetEntriesByListIdAsync(listId, hideCompleted: true);

            // Assert
            Assert.AreEqual(2, todoEntries.Count);
        }

        /// <summary>
        /// Checks if methods throw ArgumentNullException.
        /// </summary>
        [Test]
        public void Methods_Should_Throw_ArgumentNullException()
        {
            var todoList = new TodoList();
            todoList = null;

            var todoEntry = new TodoEntry();
            todoEntry = null;

            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.InsertListAsync(todoList));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.CopyListAsync(todoList));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.UpdateListAsync(todoList));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.RemoveListAsync(todoList));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.InsertEntryAsync(todoEntry));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.UpdateEntryAsync(todoEntry));
            Assert.ThrowsAsync<ArgumentNullException>(async Task () => await todoService.RemoveEntryAsync(todoEntry));
        }
    }
}