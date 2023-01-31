using AutoMapper;
using Todo.Data.Domain;
using Todo.Web.Models;

namespace Todo.Web.Infrastructure
{
    /// <summary>
    /// Class for AutoMapper profiles.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoList, TodoListViewModel>().ReverseMap();
            CreateMap<TodoEntry, TodoEntryViewModel>().ReverseMap();
        }
    }
}