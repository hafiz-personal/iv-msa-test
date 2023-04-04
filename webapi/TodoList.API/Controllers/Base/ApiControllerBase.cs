using Microsoft.AspNetCore.Mvc;

namespace TodoList.API.Controllers.Base
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
