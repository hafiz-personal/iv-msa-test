using AutoMapper;
using TodoList.Application.Services.Todos.Dto;
using TodoList.Core.Domain;

namespace TodoList.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<RequestCreateTask, Todo>();
            CreateMap<RequestUpdateTask, Todo>().ReverseMap();   
        }
    }
}
