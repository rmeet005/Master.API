using Master.Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRoleService Service;

        public RoleController(IRoleService service)
        {
            this.Service = service;

        }

        [HttpGet]
        [Route("FetchAllRoles")]
        public IActionResult FetchAllRoles()
        {
            var data = Service.FetchAllRoles();
            return Ok(data);
        }
    }
}
