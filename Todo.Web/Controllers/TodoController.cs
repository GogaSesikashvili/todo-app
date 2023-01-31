using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Domain;
using Todo.Services;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService todoService;
        private readonly IMapper mapper;
        private readonly ILogger<TodoController> logger;

        public TodoController(ITodoService todoService, IMapper mapper, ILogger<TodoController> logger)
        {
            this.todoService = todoService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> Reminder()
        {
            var reminders = await todoService.GetCurrentReminderEntriesAsync();

            return Json(reminders);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var todoLists = await todoService.GetTodoListsAsync(hidden: false);

            var model = mapper.Map<IEnumerable<TodoListViewModel>>(todoLists);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewHiddenLists()
        {
            var todoLists = await todoService.GetTodoListsAsync(hidden: true);

            var model = mapper.Map<IEnumerable<TodoListViewModel>>(todoLists);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(TodoListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoList = mapper.Map<TodoList>(model);

                    await todoService.InsertListAsync(todoList);

                    if (model.Hide)
                    {
                        return RedirectToAction("ViewHiddenLists");
                    }

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(CreateList)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ModifyList(int id)
        {
            var todoList = await todoService.GetListByIdAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoListViewModel>(todoList);

            if (todoList.Hide)
            {
                ViewBag.ViewLists = "ViewHiddenLists";
            }
            else
            {
                ViewBag.ViewLists = "Index";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyList(TodoListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoList = mapper.Map<TodoList>(model);

                    await todoService.UpdateListAsync(todoList);

                    if (model.Hide)
                    {
                        return RedirectToAction("ViewHiddenLists");
                    }

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(ModifyList)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CopyList(int id)
        {
            var todoList = await todoService.GetListByIdAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoListViewModel>(todoList);

            if (todoList.Hide)
            {
                ViewBag.ViewLists = "ViewHiddenLists";
            }
            else
            {
                ViewBag.ViewLists = "Index";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CopyList(TodoListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoList = mapper.Map<TodoList>(model);

                    await todoService.CopyListAsync(todoList);

                    if (model.Hide)
                        return RedirectToAction("ViewHiddenLists");

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(CopyList)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveList(int id)
        {
            var todoList = await todoService.GetListByIdAsync(id);

            if (todoList == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoListViewModel>(todoList);

            if (todoList.Hide)
            {
                ViewBag.ViewLists = "ViewHiddenLists";
            }
            else
            {
                ViewBag.ViewLists = "Index";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveList(TodoListViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoList = mapper.Map<TodoList>(model);

                    await todoService.RemoveListAsync(todoList);

                    if (model.Hide)
                        return RedirectToAction("ViewHiddenLists");

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(RemoveList)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewEntries(int listId)
        {
            var todoEntries = await todoService.GetEntriesByListIdAsync(listId, hideCompleted: false);

            var model = mapper.Map<IEnumerable<TodoEntryViewModel>>(todoEntries);

            ViewBag.listId = listId;

            var list = await todoService.GetListByIdAsync(listId);

            if (list.Hide)
            {
                ViewBag.ViewLists = "ViewHiddenLists";
            }
            else
            {
                ViewBag.ViewLists = "Index";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEntriesHideCompleted(int listId)
        {
            var todoEntries = await todoService.GetEntriesByListIdAsync(listId, hideCompleted: true);

            var model = mapper.Map<IEnumerable<TodoEntryViewModel>>(todoEntries);

            ViewBag.listId = listId;

            var list = await todoService.GetListByIdAsync(listId);

            if (list.Hide)
            {
                ViewBag.ViewLists = "ViewHiddenLists";
            }
            else
            {
                ViewBag.ViewLists = "Index";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEntry(int id)
        {
            var todoEnty = await todoService.GetEntryByIdAsync(id);

            var model = mapper.Map<TodoEntryViewModel>(todoEnty);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEntriesDueToday()
        {
            var todoEntries = await todoService.GetEntriesDueTodayAsync();

            var model = mapper.Map<IEnumerable<TodoEntryViewModel>>(todoEntries);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewReminderEntries()
        {
            var todoEntries = await todoService.GetReminderEntriesAsync();

            var model = mapper.Map<IEnumerable<TodoEntryViewModel>>(todoEntries);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPersonalEntries()
        {
            var todoEntries = await todoService.GetPersonalEntriesAsync();

            var model = mapper.Map<IEnumerable<TodoEntryViewModel>>(todoEntries);

            return View(model);
        }

        [HttpGet]
        public IActionResult AddEntry(int listId)
        {
            var model = new TodoEntryViewModel
            {
                TodoListId = listId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEntry(TodoEntryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoEntry = new TodoEntry
                    {
                        Title = model.Title,
                        Description = model.Description,
                        AdditionalNotes = model.AdditionalNotes,
                        Label = (Data.Domain.Label)model.Label,
                        DueDate = model.DueDate,
                        Reminder = model.Reminder,
                        IsReminded = false,
                        CreationDate = DateTime.Now,
                        Status = (Data.Domain.Status)model.Status,
                        TodoListId = model.TodoListId
                    };

                    await todoService.InsertEntryAsync(todoEntry);

                    return RedirectToAction("ViewEntries", new { listId = model.TodoListId });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(AddEntry)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ModifyEntry(int id)
        {
            var todoEntry = await todoService.GetEntryByIdAsync(id);

            if (todoEntry == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoEntryViewModel>(todoEntry);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyEntry(TodoEntryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoEntry = mapper.Map<TodoEntry>(model);

                    await todoService.UpdateEntryAsync(todoEntry);

                    return RedirectToAction("ViewEntry", new { id = model.Id });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(ModifyEntry)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEntryStatus(int id)
        {
            var todoEntry = await todoService.GetEntryByIdAsync(id);

            if (todoEntry == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoEntryViewModel>(todoEntry);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEntryStatus(TodoEntryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoEntry = mapper.Map<TodoEntry>(model);

                    await todoService.UpdateEntryAsync(todoEntry);

                    return RedirectToAction("ViewEntries", new { listId = model.TodoListId });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(ChangeEntryStatus)} - errorMessage:{ex.Message}");

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveEntry(int id)
        {
            var todoEntry = await todoService.GetEntryByIdAsync(id);

            if (todoEntry == null)
            {
                return NotFound();
            }

            var model = mapper.Map<TodoEntryViewModel>(todoEntry);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEntry(TodoEntryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var todoEntry = mapper.Map<TodoEntry>(model);

                    await todoService.RemoveEntryAsync(todoEntry);

                    return RedirectToAction("ViewEntries", new { listId = model.TodoListId });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError($"error to call {nameof(RemoveEntry)} - errorMessage:{ex.Message}");

                throw;
            }
        }
    }
}