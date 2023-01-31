using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Todo.Services;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITodoService todoService;

        public HomeController(ILogger<HomeController> logger, ITodoService todoService)
        {
            this.logger = logger;
            this.todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var todoEntriesDueToday = await todoService.GetEntriesDueTodayAsync();
            var todoEntriesWithReminders = await todoService.GetReminderEntriesAsync();
            var personalTodoEntries = await todoService.GetPersonalEntriesAsync();

            ViewBag.entriesCountDueToday = todoEntriesDueToday.Count;
            ViewBag.reminderEntriesCount = todoEntriesWithReminders.Count;
            ViewBag.personalEntriesCount = personalTodoEntries.Count;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}