using Microsoft.AspNetCore.Mvc;
using TodoList.API.Controllers.Base;

namespace TodoList.API.Controllers
{   
    public class TasksController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("test");
        }
    }
}
